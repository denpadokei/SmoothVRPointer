# SmoothVRPointer

[![Build and Release](https://github.com/denpadokei/SmoothVRPointer/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/SmoothVRPointer/actions/workflows/build-and-release.yml)

メニュー操作時の VR ポインターの動きを滑らかにする MOD です。コントローラーの位置と回転を補間して、細かな手ぶれを抑えたポインター操作にします。

## 機能

- `HMLib.dll` 内の `IVRPlatformHelper` 実装が持つ `GetNodePose` を Harmony の postfix でフックする
- 左右のコントローラーの位置を `Vector3.Lerp`、回転を `Quaternion.Slerp` で補間する
- 補間の強さを `OffsetValue` で調整できる (大きいほど追従が速く、小さいほど滑らかになる)
- ゲームプレイ中 (`GameCore` シーン) は補間を行わず、メニュー操作時のみ適用する
- 設定で機能の ON/OFF と補間値を変更できる

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.36.2 |
| 依存 MOD | BSIPA ^4.3.3 / BeatSaberMarkupLanguage ^1.11.2 / SiraUtil ^3.1.8 |

## インストール

1. Beat Saber を 1.36.2 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/SmoothVRPointer/releases) から `SmoothVRPointer-1.0.0-bs1.36.2.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

インストール後はメニュー操作時のポインターが自動で滑らかになります。ゲームプレイ中は無効になるため、ノーツカットには影響しません。追従が遅く感じる場合は設定で `OffsetValue` を大きくしてください。

## 設定

ゲームプレイ設定の `SmoothVRPointer` タブで設定します。

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| Enable | bool | true | ポインターの補間を有効にする |
| OffsetValue | float | 9.5 | 補間の強さ。設定画面のインクリメントは 0.0〜15.0、刻み 0.1 |

設定は `UserData/SmoothVRPointer.json` に保存されます。

## ビルド

```powershell
git clone https://github.com/denpadokei/SmoothVRPointer.git
cd SmoothVRPointer
dotnet build .\SmoothVRPointer\SmoothVRPointer.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
