# Path Matcher v1.0

A command-line utility for matching files and directories using glob patterns within a specified target directory.

## Supports:

- Windows x86/x64
- Linux and OSX

## Usage:

Either copy the executable file to target directory or define path. If no path is defined, the running directory is used.

### Arguments:

- -path \<directory> or -p \<directory> : Define path to target directory.
  - PatchMatcher -p "D:\Your`Path\Here"
- -patterns \<value> : Expects a comma separated list of glob patterns.
  - PathMatcher -patterns "./Data/\*\*/\*, ./Components/Options/\*\*/\*"
- -exclude \<paths> or -e \<paths> : Paths to exclude from matching
  - PathMatcher -e "./Components/Tech/ErrorHandler.cs"
- -wrap \<value> or -w \<value> : When defined the matched paths begins and ends with value. This can be useful when pasting the paths in another software, like Copilot or other AI tools
  - PathMatcher -w '
  - PathMatcher -w "
- -help or -? : Shows supported commands.

---

### Files:

- Windows x86: [exe](https://sltech.no/DISTRO/PathMatcher/Windows/x86/PathMatcher.exe) | [zip](https://sltech.no/DISTRO/PathMatcher/Windows/x86/PathMatcher.zip) | [rar](https://sltech.no/DISTRO/PathMatcher/Windows/x86/PathMatcher.rar)
- Windows x64: [exe](https://sltech.no/DISTRO/PathMatcher/Windows/x64/PathMatcher.exe) | [zip](https://sltech.no/DISTRO/PathMatcher/Windows/x64/PathMatcher.zip) | [rar](https://sltech.no/DISTRO/PathMatcher/Windows/x64/PathMatcher.rar)
- Linux : [tar](https://sltech.no/DISTRO/PathMatcher/Linux/PathMatcher.tar)
- OSX: [tar](https://sltech.no/DISTRO/PathMatcher/OSX/PathMatcher.tar)

---

SL Tech Path Matcher &copy; 2026 / [slt@sltech.no](mailto:slt@sltech.no)
