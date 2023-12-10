# HERA Touch

"HERA Touch" is a mobile application developed for **iOS** platform using the Unity Engine that enables users to remotely assign tasks to Robody and control the arms of it in the virtual environment along with the AR headset.


**Unity Editor version:** 2021.3.21f1 <br>
**[IMPORTANT]** This exact version of the Unity Editor should be used in order to make sure all the packages are compatible. <br>
**[IMPORTANT]** AR Foundation 5.0.7, Apple ARKit XR Plugin 5.0.7 are required. Make sure the version of all the packages are correct. 

For Android support, Google ARCore XR Plug-in is required instead of Apple ARKit XR Plugin. Android build support should also be added to the Unity Editor. However, the project is not validated on Android platform.

## Setup / Installation
To setup the "HERA Touch" project in your Unity Editor, follow these steps:

1. Download and extract the ZIP file / clone the repository. This will create a folder containing the Unity project files.
2. Open the project with the Unity Editor of the required version.
3. The project can be run with full functionalities in Unity Editor with the help of [AR Foundation Remote](https://assetstore.unity.com/packages/tools/utilities/ar-foundation-remote-2-0-201106). While this package is costly, building the project to an iOS device is an alternative.

To build "HERA Touch" application on an iOS device, follow these steps:

1. Set the target platform to iOS in File/Build Settings and click Build. The built files will be put in the folder at your choice.
2. The folder contains a **Unity-iPhone.xcodeproj** file. Open it with Xcode on a MacOS device.
3. Enable developer mode on the iPhone and attach it to the MacOS device.
4. Click build button. 

**[IMPORTANT]** It is important to notice that, it seems that developers should request for multicast entitlement from Apple to be granted to use UDP broadcast in local network (check [this](https://developer.apple.com/contact/request/networking-multicast)). In our case, this is mandatory for the smartphone application to communicate with the other devices.

## User Guide

The main features of the application include:
1. Interaction with AR Environment: Users can use the smartphone as an AR controller.
2. Robody Hand Control: Users can use the smartphone to control the hands of Robody.
2. Task Assignment: Users can assign tasks to Robody among predefined tasks. Current defined tasks include autonomous displacement and patrol.
3. Monitoring: Users can see what Robody sees in the virtual environment via the Monitoring page. Users can also view the position of Robody on a map.

## Troubleshooting
In case of any issues or questions about the project, contact [yinfeng.yu@tum.de](mailto:yinfeng.yu@tum.de).

## API Documentation
For further development of / modification to "HERA Touch", refer to the API documentation [here]().
