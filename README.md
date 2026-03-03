Parsers for GTK and Mutagen Colors on linux.

Parses files these following files

```
.config/gtk-3.0/colors.css
.config/gtk-4.0/colors.css
.config/qt5ct/colors/matugen.conf
.config/qt6ct/colors/matugen.conf
```

These are built out to objects that can be used and the related colors and names.

Usage
```
new Ottergen.MatugenParserQt("/home/user/.config/qt6ct/colors/matugen.conf").Parse();
  - Will Return a QtColors which contains the filed parsed out
  - Example would be colors.ColorsWindow.BackgroundAlternate
  - These contain the R G B (int) Reprsenatation of the color
new Ottergen.MatugenParserGtk("/home/user/.config/gtk-4.0/colors.css").Parse();
  - Will Return a GtkColors which contains the filed parsed out
  - Example would be colors.Header.Background
  - These contain the R G B (int) Reprsenatation of the color
```
