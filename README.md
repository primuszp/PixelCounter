# PixelCounter

| Original scan | Processed (B&W) |
|:---:|:---:|
| ![Original scan](docs/images/scan.jpg) | ![Processed result](docs/images/processed.png) |

A Windows desktop tool for measuring the area of objects on scanned images by analysing the ratio of black and white pixels. Originally designed for measuring leaf surface area from flatbed scans.

## How it works

1. Place the object (e.g. a leaf) on a white background and scan it.
2. PixelCounter converts the image to black and white using [Otsu thresholding](https://en.wikipedia.org/wiki/Otsu%27s_method).
3. It counts the black pixels and calculates the object's area — either from the DPI embedded in the image metadata, or from a manually specified paper size.

```
Area (cm²) = black pixels × (2.54 / dpiX) × (2.54 / dpiY)
```

No manual scale calibration is required when the scanned image contains DPI metadata (which all major flatbed scanners produce).

## Features

- **Automatic DPI calibration** — reads pixel resolution directly from image metadata; no manual scale setup required. Falls back to paper size if metadata is absent.
- **Batch processing** — processes all images in a selected folder at once and writes a single TSV report.
- **Otsu thresholding** — automatic black/white conversion without manual threshold tuning.
- **Hole filling** — flood-fill algorithm closes interior white regions (e.g. leaf veins) before measurement, so only the outer contour defines the area.
- **Connected component analysis** — detects and measures multiple separate objects in a single image; each object is reported on its own row.
- **Labeled output** — saves a black/white PNG with component index numbers drawn at each object's centroid, so results can be traced back to individual objects.
- **Paper size fallback** — A0–A4 presets or custom width × height in mm, used when DPI metadata is unavailable.
- **Bilingual UI** — English by default; switches to Hungarian automatically when the system locale is `hu-HU`.

## Report format

Results are written to `report.txt` (tab-separated) in the source folder.

### Normal mode

| Filename | Black Px | White Px | Other Px | Ratio (%) | Area (cm²) |
|----------|----------|----------|----------|-----------|------------|

### Component mode

| Filename | Component # | Black Px | Area (cm²) |
|----------|-------------|----------|------------|

## UI options

| Control | Description |
|---------|-------------|
| **Auto (image DPI)** | Use DPI from image metadata (recommended for scanner output) |
| **Paper size** | Fallback: select A0–A4 or enter custom dimensions in mm |
| **Black/White** | Apply Otsu thresholding before counting |
| **Fill holes** | Close interior white regions (leaf veins, holes) before measurement |
| **Components** | Detect and measure each object separately |
| **Min. size (px)** | Ignore noise below this pixel count |
| **PNG** | Save the processed black/white image (with labels in component mode) |

## Requirements

- Windows 10 / 11
- [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)

## Building from source

```bash
git clone https://github.com/your-username/PixelCounter.git
cd PixelCounter
dotnet build PixelCounter/PixelCounter.csproj
```

## Supported image formats

`.bmp` `.png` `.jpg` `.gif` `.tiff` `.tif`

## License

MIT
