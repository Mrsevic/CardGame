# Card Game

Description: Simple card game

## Build instructions
#### Prerequisites 
Dot net core 3.1 SDK should be installed on your computing device with minimum x86/ARM V7 architecture(RPI Zero for instance is not supported).
For the further details please see [the following link ](https://dotnet.microsoft.com/download/dotnet-core/3.1).
#### Build process
Clone the repository, navigate to the root of CardGame project(not solution) and run the following command in your terminal:  
```
dotnet publish -c Release /p:Configuration=Release
```
Appropriate files will be generated for your platform of choice in the:   
\project\bin\Release\netcoreapp3.1\publish.  
 You can use them to run the appplication(for windows platform you will find .exe file).

## Tests
Navigate to the root of CardGame.Tests project and run:
```
dotnet test command
```
Alternatively you can run the tests in the Visual Studio text explorer or by positioning the cursor inside the individual test and pressing Ctrl + R, T key combination. Playlist files are also provided for your convinience.