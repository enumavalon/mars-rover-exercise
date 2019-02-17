# Mars Rover Exercise
A console application that retrieves images captured by the Mars Rovers through the NASA API and stores the images locally.

## API 
The console app uses the API ` https://mars-photos.herokuapp.com/ ` because it doesn't require an API key.

## ImageRetriever
Retrieves the photos given the name of the rover and a requested date.

## FileHelper
Responsible for saving the retrieved images in a local folder.

## Save Location
All retrieved images will be stored in the same folder as the executing assembly inside the folder path ` $\\Photos\{rovername}\{daterequested} `
