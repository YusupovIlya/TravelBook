## Description

TravelBook is an application that allows to record and structure travel information.
<p align="center"><img src="https://github.com/YusupovIlya/TravelBook/blob/master/ScreenShots/logo.jpg" width="40%"></p>

## Getting started

### Build application


1. Clone the `TravelBook` repository:
```bash
git clone https://github.com/YusupovIlya/TravelBook.git
```
2. Go to the project folder and build the project using docker:
```bash
cd TravelBook
docker compose up
```
### Logging in

Application is available on `http://<hostname>:5001`. To log in, use default credentials `my@email.com`/`superpassword`.
You can also create a new account on the registration page available on `http://<hostname>:5001/account/register`.

## Usage notes

### Travels
Any registered and authorized user can add note about itself travel:
<p align="center"><img src="https://github.com/YusupovIlya/TravelBook/blob/master/ScreenShots/travel.jpg" width="70%"></p>

Any travel can include (the user cannot add anything from the list below without a travel record):
* Photo albums
* Articles (notes)

### Photo albums
In the photo album, user can add or delete photos, it is also possible to add a description and a place to a selected photo:
<p align="center"><img src="https://github.com/YusupovIlya/TravelBook/blob/master/ScreenShots/photoalbum.jpg" width="70%"></p>

### Articles
User can add articles using the text editor (for example, highlight text or add a bulleted list):
<p align="center"><img src="https://github.com/YusupovIlya/TravelBook/blob/master/ScreenShots/article.jpg" width="70%"></p>

## My thoughts for improvement
* The ability to make your photos or articles public (something like a social network)
* Likes for posts or photos
* An administrator, with the corresponding functions
* ...