.PHONY: clean run

DEPENDENCIES = /reference:System.Windows.Forms.dll \
	/reference:System.Drawing.dll \
	/reference:System.Data.dll

CS_FILES := $(shell find src -name *.cs)

ASSETS := $(shell ls -d assets)

build/Program.exe: $(CS_FILES)
	mkdir -p build
	mcs $(DEPENDENCIES) app/Program.cs $(CS_FILES)
	mv app/Program.exe build/Program.exe

run: build/Program.exe $(ASSETS)
	cp -R assets build/
	cd build && mono Program.exe

clean:
	rm -rf build
