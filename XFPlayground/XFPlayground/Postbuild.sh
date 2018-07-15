SECONDS=0;
dotnet run --no-restore --no-dependencies --no-build  --verbosity m --project $HOME/XFPlayground/UserInterfaceBuilder/UserInterfaceBuilderPreserver/UserInterfaceBuilderPreserver.csproj
echo :"Time taken to execute: $((($SECONDS / 60) % 60))min $(($SECONDS % 60))sec"

#msbuild /p:Configuration=Build /verbosity:minimal $HOME/UserInterfaceBuilder/UserInterfaceBuilderPreserver/UserInterfaceBuilderPreserver.csproj ; "/usr/local/share/dotnet/dotnet"  $HOME/UserInterfaceBuilder/UserInterfaceBuilderPreserver/bin/Build/netcoreapp2.1/UserInterfaceBuilderPreserver.dll;

#/Users/arun/XFPlayground/XFPlayground/Postbuild.sh

#Open terminal and run this : sh $HOME/XamlPlayground/XamlPlayground/References/postbuild.sh

#dotnet run --no-restore --no-dependencies --no-build  --verbosity m --project $HOME/UserInterfaceBuilder/UserInterfaceBuilderPreserver/UserInterfaceBuilderPreserver.csproj


#<ContentPage xmlns:ios="clr-namespace:Xamarin.Forms.PlatformConfiguration.iOSSpecific;assembly=Xamarin.Forms.Core" ios:Page.UseSafeArea="true" xmlns:elem="clr-namespace:XamarinFormsStarterKit.UserInterfaceBuilder.UIElements;assembly=UserInterfaceBuilder" xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="XamarinFormsStarterKit.UserInterfaceBuilder.XamlPlayground.Playground">
