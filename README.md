# Hibzz DefineManager

![LICENSE](https://img.shields.io/badge/LICENSE-CC--BY--4.0-ee5b32?style=for-the-badge) [![Twitter Follow](https://img.shields.io/badge/follow-%40hibzzgames-1DA1f2?logo=twitter&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/YXdJ8cZngB) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

 A tool used to manage #defines in a project to enable/disable optional features in supported packages and to produce cleaner and more efficient code. It looks and feels close to Unity's package manager, making the tool easy for users to pick up and use.
 
 ### Installation
**Via NPM**
This package is published to the NPM registry, so users can install and get updates directly in the Unity Package Manager when the package is installed via NPM.
- Navigate to the advanced project settings menu in the Unity Package Manager
- Create a new scoped registry with the URL `https://registry.npmjs.org`
- Add `com.hibzz.definemanager` as a scope
- Now you'll be able to view and install the package under "My Registeries" in the Package Manager.

**Via Github**
This package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/hibzzgames/Hibzz.DefineManager.git
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

## Have a question or want to contribute?
If you have any questions or want to contribute, feel free to join the [Discord server](https://discord.gg/YXdJ8cZngB) or [Twitter](https://twitter.com/hibzzgames). I'm always looking for feedback and ways to improve this tool. Thanks!

Additionally, you can support the development of these open-source projects via [GitHub Sponsors](https://github.com/sponsors/sliptrixx) and gain early access to the projects.


