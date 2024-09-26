# Project Variables
PROJECT_NAME ?= SolarCoffee
ORG_NAME ?= SolarCoffee
REPO_NAME ?= SolarCoffee

#especify the targets
.PHONY: migrations db

migrations:
	cd ./backend/SolarCoffee.Infrastructure && dotnet ef --startup-project ../SolarCoffee.API migrations add $(mname) && cd ../..

db:
	cd ./backend/SolarCoffee.Infrastructure && dotnet ef --startup-project ../SolarCoffee.API database update && cd ../..