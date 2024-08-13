# Sardinal

![](https://img.shields.io/badge/unity-2022.3+-000.svg)
[![Releases](https://img.shields.io/github/release/hoshinolabs/Localization.svg)](https://github.com/hoshinolabs/Localization/releases)

Sardinal is a messaging system for VRChat Udon#. It supports Pub/Sub with parameters.  
  
SardinalはVRChat Udon#用のメッセージングシステムです。  
パラメータ付きのPub/Subをサポートしています。

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
public class HelloSardine : UdonSharpBehaviour {
  [Subscriber(typeof(SardineSignal))]
  public void Hello(string arg) {
    Debug.Log($"Hello {arg}.");
  }
}
```

```csharp
public abstract class SardineSignal { }

public class SardinalDemo : UdonSharpBehaviour {
  [Inject, SerializeField, HideInInspector]
  ISardinal sardinal;
  [SerializeField]
  SignalId signalId = new SignalId<SardineSignal>();

  private void Start() {
    sardinal.Publish(signalId, null, $"Sardinal");
  }

#if UNITY_EDITOR
  public class Builder : IProcessSceneWithReport {
    public int callbackOrder => 0;

    public void OnProcessScene(Scene scene, BuildReport report) {
      var context = ProjectContext.New();
      context.Enqueue(builder => {
        builder.AddInHierarchy<SardinalDemo>();
      });
      context.Build();
    }
  }
#endif
}
```

実行するとコンソールに以下のように表示されます。
```bash
Hello Sardinal.
```

## Author

[@ikuko](https://x.com/magi_ikuko)

## License

MIT  

利用頂いた際は使用ワールドなどを教えて頂けると幸いです。  
また任意ですがクレジットの掲載もして頂けると嬉しいです。
