# Snake Game

Replica of the OG Snake game we all loved and adored made using the Unity game engine.

Download here: https://play.google.com/store/apps/details?id=com.gspteck.snakegame

## Publishing

Open this project in **Unity 6000.0.85f1** or a newer Unity 6000.0 patch. Older 6000.0 editors cannot target Android 16 or build an App Store archive with Xcode 26.

The Play package name stays `com.gspteck.snakegame`. The iOS bundle id is the same. This repo is version **1.1.0**, Android version code **3**, and iOS build **1**. If Play Console already has a version code of 3 or higher, raise `AndroidBundleVersionCode` above that live value before you upload.

### Android App Bundle

1. Install Unity 6000.0.85f1 with Android Build Support (SDK 36, NDK r27c).
2. Let Package Manager download Appodeal 4.4.0 and External Dependency Manager 1.2.190.
3. Switch the build target to Android. External Dependency Manager should resolve `Assets/Appodeal/Editor/Dependencies/AppodealDependencies.xml`. If it does not, use Assets → External Dependency Manager → Android Resolver → Force Resolve.
4. Build an **Android App Bundle** (`.aab`), not an APK. Player Settings already target API 36, ARM64 only, and minimum API 26.
5. Sign with the existing dedicated keystore `snakegame.jks`, alias `snakegame`. Do not create a new upload key. A different key is rejected unless you reset the upload key in Play App Signing.
6. In Play Console, complete Data safety (ads and advertising ID) and set a privacy-policy URL. That page must link to [Appodeal’s privacy policy](https://www.appodeal.com/privacy-policy).

`Assets/Icons/icon.png` is 512×512 and has transparency, so it is not a valid iOS App Store icon and is not assigned to the store icon slots. Upload a 512×512 Play listing icon separately, and add an opaque 1024×1024 icon before the iOS archive.

### AdMob

AdMob and Google Ad Manager are left out of the dependency file, and the game disables those networks at startup. The IDs in Appodeal Settings are still placeholders. Paste the real Android and iOS AdMob app IDs into Appodeal → Appodeal Settings, then add the AdMob adapters back in Appodeal → Plugin Configuration, before you want AdMob demand. Shipping the placeholder ID crashes on launch.

### iOS

1. Install the same Unity editor with iOS Build Support, on a Mac with **Xcode 26** and the iOS 26 SDK.
2. Player Settings target iOS 15, which is the Appodeal 4.4 minimum. Automatic signing is on. Enter your Apple Team ID before you archive.
3. Resolve iOS dependencies (Swift packages, with CocoaPods still enabled for adapters that have no Swift package).
4. Build the Xcode project and archive it. App Store Connect requires the Xcode 26 / iOS 26 SDK.
5. Appodeal Settings enable `NSUserTrackingUsageDescription`. The consent manager shows the tracking prompt on iOS 14.5 and later. In App Review notes, say that the prompt appears on first launch.
6. Use the same privacy-policy URL as Google Play.

Version code, keystore passwords, the Apple Team ID, AdMob app IDs, and the privacy-policy URL are not in this repository.

## Other

Support us by buying awsome merch on our [Teespring page](https://teespring.com/it/stores/gspteck-2).

If you find any problems with code or have ANY suggestions for improvements and/or ideas for future projects,
Please send us an email at 'gspteck@gmail.com'.
We will review it and try our best to fix the code.

Want to support us?

BTC: 1Q1ZFsZCXhan1QG9Su1S9xEX75WpecYE3C

ETH: 0x3F21aBAd969f9c8B18975B8cb9f039569bF10f0D

BCH: bitcoincash:qrdv0y0lgasxs39kwmuvglsduztvmpcrhc24t68p8a

PayPal: [gspteck@gmail.com](https://paypal.me/gspteck)

or just credit us when you use our code (very much appreciated as well).

Sincerely,
GSPTeck
