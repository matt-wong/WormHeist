from PIL import Image

# Open an image
img = Image.open('path/to/image.png')

# Resize
img_resized = img.resize((800, 600))

# Rotate
img_rotated = img.rotate(45)

# Save
img_resized.save('output.png')

# Get image info
print(img.size)  # (width, height)
print(img.format)  # 'PNG', 'JPEG', etc.
print(img.mode)  # 'RGB', 'RGBA', etc.s