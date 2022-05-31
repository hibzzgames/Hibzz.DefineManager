# Hibzz DefineManager

![npm](https://img.shields.io/npm/dt/com.hibzz.definemanager?color=fb8717&style=for-the-badge) ![LICENSE](https://img.shields.io/badge/LICENSE-MIT-b8c3b7?style=for-the-badge) [![Twitter Follow](https://img.shields.io/twitter/follow/hibzzgames?color=1a8cd8&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/tZdZFK7) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) [![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/sliptrixx)

 A tool used to manage #defines in a project to enable/disable optional features in supported packages and to produce cleaner and more efficient code. It looks and feels close to Unity's package manager, making the tool easy for the users to pick up and use.
 
 ### Installation
**Via NPM**
This package is published to the NPM registery, so users can install and get updates directly in the Unity Package Manager when the package is installed via NPM.
- Navigate to the advanced project settings menu in the Unity Package Manager
- Create a new scoped registry with the URL `https://registry.npmjs.org`
- Add `com.hibzz.definemanager` as scope
- Now you'll be able to view and install the package under the "My Registeries" in the Package Manager.

**Via Github**
This package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/Hibzz-Games/Hibzz.DefineManager.git
```

### Reference Image
![image](https://user-images.githubusercontent.com/37605842/168430623-b73c373e-3397-4b69-a818-10d610f69a4c.png)

### How to use?
- Navigate to Windows -> Define Manager
- [First Time Use] Click "Initialize Define Manager" to initialize the tool
- Select the define that needs to be installed/removed
- Press the install/remove button!

### For Package Developers - How to Use?

**Registering a new define**
```c#
#if ENABLE_DEFINE_MANAGER

[RegisterDefine]
static DefineRegistrationData RegisterController()
{
    DefineRegistrationData data = new DefineRegistrationData();
    
    data.Define = "ENABLE_CONTROLLER";
    data.DisplayName = "Controller Support";
    data.Category = "Game";
    data.Description = "Enable the controller and rumble support for devices that support Xbox controllers. ";
    data.EnableByDefault = true;
    
    return data;
}

#endif
```

