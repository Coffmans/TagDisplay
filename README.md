[![Typing SVG](https://readme-typing-svg.demolab.com?font=Fira+Code&pause=1000&color=3A13F7&size=24&center=true&width=1000&lines=****+TagDisplay+-+A+Utility+Designed+To+Display+Audio+Metadata+****)](https://git.io/typing-svg)

One day, I needed to view the embedded metadata tags for a handful of audio files. I had an MP3 file and I wanted to view the ID3 data. I had a WAV file and I wanted to view the CartChunk data. I could have easily downloaded an application that allowed for the viewing and updating of audio metadata. But I didn't need such a large, robust application. I needed a small and quick utility that could give me the data needed without the bloat.

I decided to create my own utility. Nothing fancy, a simple utility that reads audio files for a directory (and sub-directories if desired). The user can double-click on the file and the metadata will be displayed. Metadata such as title, artist, album, comments, composer, genre, and more. Some of the audio formats supported in this utility include:
* MP3
* WAV
* CartChunk
* BWF
* APE
* FLAC
* OGG
* MP4

The utility utilizes BASS.DLL and BASS.NET for the retrieval of the audio metadata. Some of the formats, such as FLAC and APE, are still under development.

![image](https://user-images.githubusercontent.com/8136145/205948773-52690d6f-a9af-415b-a058-abcf855360db.png)

![image](https://user-images.githubusercontent.com/8136145/205948819-6be1a430-3411-4f03-857a-dc285a7efaad.png)
