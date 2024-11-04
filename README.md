# Sardinal

![](https://img.shields.io/badge/unity-2022.3+-000.svg)
[![Releases](https://img.shields.io/github/release/hoshinolabs/Sardinal.svg)](https://github.com/hoshinolabs/Sardinal/releases)

Sardinal is a messaging system for <a href="https://udonsharp.docs.vrchat.com/">VRChat Udon(U#)</a>. It supports Pub/Sub with parameters.  
  
Sardinal は <a href="https://udonsharp.docs.vrchat.com/">VRChat Udon(U#)</a> 用のメッセージングシステムです。  
パラメータ付きの Pub/Sub をサポートしています。

## Features

## Documentation

~~View on [GitHub Pages](https://sardinal.github.io)~~

## Installation

*Unity 2022.3+ が必要です*

### VCC を利用したインストール

1. 次のページを開き、「Add to VCC」を押します。  
  [HoshinoLabs VPM Repository](https://vpm.hoshinolabs.com/)
2. VCCの「Manage Project」を押す。
3. `HoshinoLabs - Sardinal` の横の「+」ボタンを押す。

### Install commandline (using VPM CLI)

```bash
vpm add repo https://vpm.hoshinolabs.com/vpm.json
cd /your-unity-project
vpm add com.hoshinolabs.sardinal
```

### Install manually (using .unitypackage)

1. Download the .unitypackage from [releases](https://github.com/hoshinolabs-vrchat/Sardinal/releases) page.
2. Open .unitypackage

## Basic Usage

次のような Udon があるとする。

```csharp
public interface MySignal { }
```

```csharp
public class HelloSardine : UdonSharpBehaviour {
  [Subscriber(typeof(MySignal))]
  public void Hello(string arg) {
    Debug.Log($"Hello {arg}.");
  }
}
```

```csharp
public class SardinalDemo : UdonSharpBehaviour {
  [Inject, SerializeField, HideInInspector]
  ISardinal sardinal;
  [SerializeField]
  SignalId signalId = new SignalId<MySignal>();

  private void Start() {
    sardinal.Publish(signalId, $"Sardinal");
  }
}
```

Sardinal は DI が前提のシステムとなっています。  
依存関係を記述したスクリプトを作成します。  
シーン上にオブジェクトを作成し `SardinalDemo` コンポーネントを追加します。  
あわせて `SceneScope` コンポーネントと作成したスクリプトを追加します。

```csharp
public class CustomInstaller : MonoBehaviour, IInstaller {
  public void Install(ContainerBuilder containerBuilder) {
    containerBuilder.UseComponents(transform, builder => {
      builder.RegisterOnNewGameObject<HelloSardine>(Lifetime.Cached);
      builder.RegisterInHierarchy<SardinalDemo>(Lifetime.Cached);
    });
  }
}
```

実行するとコンソールに以下のように表示されます。

```bash
Hello Sardinal.
```

例ではメッセージの受信先の存在を知らなくても送信することができています。  
相手は複数いても ID が同じ受信先にメッセージが送信されます。  
オプションで channel を利用することで受信先の範囲を指定したり動的に Subscribe することもできます。

## Author

[@ikuko](https://x.com/magi_ikuko)

## License

MIT  

利用頂いた際は使用ワールドなどを教えて頂けると幸いです。  
また任意ですがクレジットの掲載もして頂けると嬉しいです。
