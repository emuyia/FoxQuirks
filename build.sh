#!/bin/bash

rm -f *.zip
dotnet clean
dotnet build -c Debug

VERSION=$(grep -o '<Version>[^<]*' FoxQuirks.csproj | cut -d'>' -f2)
NAME=$(grep -o '<AssemblyName>[^<]*' FoxQuirks.csproj | cut -d'>' -f2)

sed -i "s/\"version_number\": \"[^\"]*\"/\"version_number\": \"$VERSION\"/" manifest.json

mkdir -p package/BepInEx/plugins
cp bin/Debug/netstandard2.1/*.dll package/BepInEx/plugins/
cp README.md CHANGELOG.md package/
cp Resources/icon.png package/
cp manifest.json package/

TEST_DIR="/Users/em/Documents/com.kesomannen.gale/lethal-company/profiles/dev/BepInEx/plugins/$NAME"
mkdir -p "$TEST_DIR"
cp bin/Debug/netstandard2.1/*.dll "$TEST_DIR/"
echo ""
echo "Copied DLLs to '$TEST_DIR' for testing."
echo ""

cd package
zip -r ../$NAME-$VERSION.zip *
cd ..
rm -rf package

echo ""
echo "Built '$NAME-$VERSION.zip'."
