.PHONY: build clean restore test run pack help

help:
	@echo "Available targets:"
	@echo "  build    - Build the solution"
	@echo "  clean    - Clean build artifacts"
	@echo "  restore  - Restore NuGet packages"
	@echo "  test     - Run tests"
	@echo "  run      - Run the examples project"
	@echo "  pack     - Create NuGet package"
	@echo "  help     - Show this help message"

build:
	dotnet build CheckClauses.sln

clean:
	dotnet clean CheckClauses.sln
	find . -type d -name "bin" -o -name "obj" | xargs rm -rf

restore:
	dotnet restore CheckClauses.sln

test:
	dotnet test CheckClauses.sln

run:
	dotnet run --project examples/CheckClauses.Examples/CheckClauses.Examples.csproj

pack:
	dotnet pack src/CheckClauses/CheckClauses.csproj --configuration Release
