# .NET Version Properties Explained

## 🎯 The Four Version Properties

.NET assemblies and NuGet packages have **four different version properties**, each serving a specific purpose:

| Property | Purpose | Format | Example | When to Change |
|----------|---------|--------|---------|----------------|
| **Version** / **PackageVersion** | NuGet package version | SemVer | `1.2.3-beta.1` | Every release |
| **AssemblyVersion** | Binary compatibility | `MAJOR.0.0.0` | `1.0.0.0` | Breaking changes only |
| **FileVersion** | File diagnostics | `MAJOR.MINOR.PATCH.BUILD` | `1.2.3.456` | Every build |
| **InformationalVersion** | Display version | Free text | `1.2.3-beta.1+sha.abc1234` | Every build |

## 📚 Detailed Breakdown

### 1. Version / PackageVersion

**Purpose:** Identifies the NuGet package version

**Format:** Full Semantic Versioning (SemVer)
```
MAJOR.MINOR.PATCH[-PRERELEASE][+METADATA]
```

**Examples:**
- `1.0.0` - Initial release
- `1.2.3` - Feature release
- `2.0.0-beta.1` - Beta prerelease
- `1.5.0-rc.2+build.123` - Release candidate with metadata

**Used by:**
- NuGet package manager
- Package restore
- Dependency resolution

**Best Practice:** 
✅ Change on every release
✅ Include prerelease identifiers for non-stable releases
✅ Follow SemVer strictly

---

### 2. AssemblyVersion

**Purpose:** .NET runtime uses this for **strong-name signing** and **assembly binding**

**Format:** ALWAYS `MAJOR.0.0.0`
```
MAJOR.0.0.0
```

**Examples:**
- v1.0.0 → AssemblyVersion: `1.0.0.0`
- v1.2.3 → AssemblyVersion: `1.0.0.0` (still!)
- v1.9.99 → AssemblyVersion: `1.0.0.0` (still!)
- v2.0.0 → AssemblyVersion: `2.0.0.0` (⚠️ breaking change)

**Critical Rules:**
⚠️ **Only change on BREAKING changes** (major version bump)
⚠️ Changing this breaks binary compatibility
⚠️ All apps must recompile and update binding redirects

**Why keep it stable?**
```csharp
// App compiled against CheckClauses 1.0.0 (AssemblyVersion 1.0.0.0)
// Can use CheckClauses 1.9.0 (AssemblyVersion 1.0.0.0) without recompiling!
// Cannot use CheckClauses 2.0.0 (AssemblyVersion 2.0.0.0) - binding error!
```

**Best Practice:**
✅ Set to `MAJOR.0.0.0`
✅ Only increment MAJOR on breaking API changes
❌ Never include MINOR or PATCH versions

---

### 3. FileVersion

**Purpose:** Windows File Properties version (Right-click DLL → Properties → Details)

**Format:** 4-part numeric version
```
MAJOR.MINOR.PATCH.BUILD
```

**Examples:**
- `1.2.3.456` - Build 456 of version 1.2.3
- `1.0.0.1234` - Build 1234 of version 1.0.0

**Used by:**
- Windows Explorer file properties
- Diagnostic tools
- Build tracking
- Support/debugging (customers can send their DLL version)

**Best Practice:**
✅ Include build number for uniqueness
✅ Can be different on every build
✅ Must be numeric only (no letters or `-beta`)

---

### 4. InformationalVersion

**Purpose:** Human-readable version string for display and diagnostics

**Format:** Free text (no restrictions)
```
Anything you want!
```

**Examples:**
- `1.2.3-beta.1+sha.abc1234.20260309`
- `1.0.0 (Built from commit abc1234 on 2026-03-09)`
- `2.0.0-rc.1+branch.release`

**Used by:**
- `Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()`
- Application logs
- About dialogs
- Crash reports

**Best Practice:**
✅ Include all metadata (commit SHA, build date, branch)
✅ Most detailed version string
✅ Helps with debugging and support

---

## 🔧 Our Implementation

### For CI Builds (Every Push)

```yaml
Version:                 0.1.0-ci.123+abc1234
PackageVersion:          0.1.0-ci.123+abc1234
AssemblyVersion:         0.0.0.0
FileVersion:             0.1.0.123
InformationalVersion:    0.1.0-ci.123+abc1234567890abcdef
```

### For Release v1.0.0

```yaml
Version:                 1.0.0
PackageVersion:          1.0.0
AssemblyVersion:         1.0.0.0
FileVersion:             1.0.0.456
InformationalVersion:    1.0.0+sha.abc1234
```

### For Release v1.2.3

```yaml
Version:                 1.2.3
PackageVersion:          1.2.3
AssemblyVersion:         1.0.0.0        ← Still 1.0.0.0!
FileVersion:             1.2.3.789
InformationalVersion:    1.2.3+sha.def5678
```

### For Release v2.0.0 (Breaking Change)

```yaml
Version:                 2.0.0
PackageVersion:          2.0.0
AssemblyVersion:         2.0.0.0        ← Changed! Breaking!
FileVersion:             2.0.0.1024
InformationalVersion:    2.0.0+sha.fed9876
```

### For Prerelease v1.5.0-beta.1

```yaml
Version:                 1.5.0-beta.1
PackageVersion:          1.5.0-beta.1
AssemblyVersion:         1.0.0.0
FileVersion:             1.5.0.1156     ← No -beta (numeric only)
InformationalVersion:    1.5.0-beta.1+sha.9876543
```

---

## 🎬 Practical Examples

### Scenario 1: Library Evolution (No Breaking Changes)

```
v1.0.0 Release
├─ Version:          1.0.0
├─ AssemblyVersion:  1.0.0.0  ←┐
├─ FileVersion:      1.0.0.100  │
└─ Info:             1.0.0      │
                                │
v1.1.0 Release (new feature)   │
├─ Version:          1.1.0      │
├─ AssemblyVersion:  1.0.0.0  ←┤ Same! Compatible!
├─ FileVersion:      1.1.0.200  │
└─ Info:             1.1.0      │
                                │
v1.2.0 Release (more features) │
├─ Version:          1.2.0      │
├─ AssemblyVersion:  1.0.0.0  ←┘ Still same!
├─ FileVersion:      1.2.0.300
└─ Info:             1.2.0

Result: Apps compiled against 1.0.0 work with 1.2.0 DLL without recompiling!
```

### Scenario 2: Breaking Change

```
v1.9.0 Release
├─ Version:          1.9.0
├─ AssemblyVersion:  1.0.0.0  ← Last compatible version
├─ FileVersion:      1.9.0.500
└─ Info:             1.9.0

v2.0.0 Release (BREAKING API CHANGE)
├─ Version:          2.0.0
├─ AssemblyVersion:  2.0.0.0  ← CHANGED! Binary incompatible!
├─ FileVersion:      2.0.0.600
└─ Info:             2.0.0

Result: Apps must be recompiled and updated to use v2.0.0
```

---

## ⚙️ How to Set in .csproj

### Default (Local Development)

```xml
<PropertyGroup>
  <Version>0.1.0</Version>
  <AssemblyVersion>0.0.0.0</AssemblyVersion>
  <FileVersion>0.1.0.0</FileVersion>
  <!-- InformationalVersion defaults to Version if not set -->
</PropertyGroup>
```

### Override at Build Time (CI/CD)

```bash
dotnet pack \
  /p:Version=1.2.3 \
  /p:PackageVersion=1.2.3 \
  /p:AssemblyVersion=1.0.0.0 \
  /p:FileVersion=1.2.3.456 \
  /p:InformationalVersion="1.2.3+sha.abc1234"
```

---

## 🚨 Common Mistakes

### ❌ Mistake 1: Setting AssemblyVersion = PackageVersion

```xml
<!-- DON'T DO THIS -->
<AssemblyVersion>1.2.3.0</AssemblyVersion>  <!-- Changes every release! -->
```

**Problem:** Every release breaks binary compatibility
**Solution:** Keep `AssemblyVersion` at `MAJOR.0.0.0`

### ❌ Mistake 2: Using Prerelease in FileVersion

```bash
# DON'T DO THIS
/p:FileVersion=1.2.3-beta.1  # ❌ Invalid! Not numeric!
```

**Problem:** FileVersion must be numeric only
**Solution:** Strip prerelease: `1.2.3.0`

### ❌ Mistake 3: No Build Number in FileVersion

```xml
<!-- DON'T DO THIS -->
<FileVersion>1.2.3.0</FileVersion>  <!-- Same for every build! -->
```

**Problem:** Can't distinguish between builds of same version
**Solution:** Add build/commit count: `1.2.3.456`

---

## 📖 References

- [Assembly Versioning (Microsoft Docs)](https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/versioning)
- [SemVer Specification](https://semver.org/)
- [NuGet Package Versioning](https://learn.microsoft.com/en-us/nuget/concepts/package-versioning)
- [AssemblyInfo Attributes](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assemblyinformationalversionattribute)

---

## 🎯 Summary

| When | Version | AssemblyVersion | FileVersion | InformationalVersion |
|------|---------|-----------------|-------------|---------------------|
| **Every Build** | ✅ CI version | ❌ Stay stable | ✅ Increment | ✅ Add SHA |
| **Minor Release** | ✅ Bump MINOR | ❌ Stay stable | ✅ New version | ✅ Full SemVer |
| **Patch Release** | ✅ Bump PATCH | ❌ Stay stable | ✅ New version | ✅ Full SemVer |
| **Breaking Change** | ✅ Bump MAJOR | ✅ **Bump MAJOR only** | ✅ New version | ✅ Full SemVer |
| **Prerelease** | ✅ Add `-beta` | ❌ Stay stable | ✅ No suffix | ✅ Full SemVer |

**Golden Rule:** Only change `AssemblyVersion` when you're okay with breaking all apps using your library!
