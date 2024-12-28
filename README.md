# Sardinal

![](https://img.shields.io/badge/unity-2022.3+-000.svg)
[![Releases](https://img.shields.io/github/release/ikuko/Sardinal.svg)](https://github.com/ikuko/Sardinal/releases)

Sardinal is a messaging system for <a href="https://unity.com/">Unity C#</a>, <a href="https://udonsharp.docs.vrchat.com/">VRChat Udon(U#)</a>. It supports Pub/Sub with parameters.  
  
Sardinal は <a href="https://unity.com/">Unity C#</a>、<a href="https://udonsharp.docs.vrchat.com/">VRChat Udon(U#)</a> 用のメッセージングシステムです。  
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

1. Download the .unitypackage from [releases](https://github.com/ikuko/Sardinal/releases) page.
2. Open .unitypackage

### Install manually (UPM)

以下を UPM でインストールします。

```
https://github.com/ikuko/Sardinal.git?path=Packages/com.hoshinolabs.sardinal
```

Sardinal はリリースタグを使用するので以下のようにバージョンを指定できます。

```
https://github.com/ikuko/Sardinal.git?path=Packages/com.hoshinolabs.sardinal#1.0.0
```

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
  [SerializeField]
  Signal signal = new Signal<MySignal>();

  private void Start() {
    signal.Publish($"Sardinal");
  }
}
```

依存関係を記述したスクリプトを作成します。  
シーン上にオブジェクトを作成し `SceneScope` コンポーネントと作成したスクリプトを追加します。

```csharp
public class CustomInstaller : MonoBehaviour, IInstaller {
  public void Install(ContainerBuilder builder) {
    builder.RegisterEntryPoint<HelloSardine>(Lifetime.Cached);
    builder.RegisterEntryPoint<SardinalDemo>(Lifetime.Cached);
  }
}
```

実行するとコンソールに以下のように表示されます。

```bash
Hello Sardinal.
```

例ではメッセージの受信先の存在を知らなくても送信することが確認できます。

## Basic Usage (dynamic subscribe)

シーンに最初から存在するサブスクライバは自動でサブスクライブされます。  
コンテナなどで動的に生成されたサブスクライバは手動でサブスクライブが必要です。

次のような Udon があるとします。

```csharp
public interface MySignal { }
```

```csharp
public class HelloSardine : UdonSharpBehaviour {
  [SerializeField]
  Signal signal = new Signal<MySignal>();

  private void Start() {
    Subscribe();
    SendCustomEventDelayedSeconds(nameof(Unsubscribe), 5f);
  }

  public void Subscribe() {
    signal.Subscribe(this);
  }

  public void Unsubscribe() {
    signal.Unsubscribe(this);
  }

  [Subscriber(typeof(MySignal))]
  public void Hello(string arg) {
    Debug.Log($"Hello {arg}.");
  }
}
```

```csharp
public class SardinalDemo : UdonSharpBehaviour {
  [SerializeField]
  Signal signal = new Signal<MySignal>();

  private void Start() {
    Publish();
  }

  public void Publish() {
    signal.Publish($"Sardinal");
    SendCustomEventDelayedSeconds(nameof(Publish), 1f);
  }
}
```

依存関係を記述したスクリプトを作成します。  
シーン上にオブジェクトを作成し  `SceneScope` コンポーネントと作成したスクリプトを追加します。

```csharp
public class CustomInstaller : MonoBehaviour, IInstaller {
  public void Install(ContainerBuilder builder) {
    builder.RegisterEntryPoint<HelloSardine>(Lifetime.Cached);
    builder.RegisterEntryPoint<SardinalDemo>(Lifetime.Cached);
  }
}
```

実行するとコンソールに以下のように表示されます。

```bash
Hello Sardinal.
Hello Sardinal.
Hello Sardinal.
```

例では開始されると受信側はサブスクライブを開始し送信側は1秒ごとにメッセージを送信しています。  
受信側は5秒立つとアンサブスクライブしメッセージの受信を終了しています。

以上のように実行時にサブスクライブすることで動的に生成されたオブジェクト間でメッセージのやり取りを行うことができます。  

## Author

[@ikuko](https://x.com/magi_ikuko)

## License

MIT  

利用頂いた際は使用ワールドなどを教えて頂けると幸いです。  
また任意ですがクレジットの掲載もして頂けると嬉しいです。
