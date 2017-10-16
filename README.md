# ReadMe
A course project for ASP .NET MVC course in Telerik Academy
## Summary
This app is for all book lovers who want to browse through infinite number of books. It provides search engine to search books by author, title and year of publishing and filter them by genre. Once you have found the book you're looking for you can mark it as Read, Want to Read or Currently Reading and update this status as much as you want. And when you have strong opinion of a book you can also rate it or leave a review to share it with everyone. Your profile keeps all your collections of books and reviews.

## Description
The project allows for a visitor to be a normal user or an admin which gives one certain privileges.

### Public part
It is visible to everyone who visits the app. Not logged users can search books by all criteria and filter them by genres. They can see the details for a book including its reviews. They can also see details for book authors.

### Private part
Registered users have personal profile page keeping their personal details, collections of books and posted reviews. One can edit their personal details and delete posted reviews. Once registered you can see other users' profile pages. Users can also see details of books and authors and most importantly - rate books, manage their collection of books by mark a book it as Read, Want to Read or Currently Reading and share their opinion of a book by posting a review.

### Administration area
Admins have administrative acces to the app. They can create, edit, delete and restore genres, books, authors and publishers. They can delete, restore users and also make them admins and remove them from admins.

## Technologies/frameworks used
* ASP .NET MVC
* Razor template engine
* Entity Framework
* MS SQL Server as database
* Bootstrap
* Ninject as IoC container
* PagedList for pagination

## Architecture
* Data layer
* Services layer
* Factories layer
* Authentication
* Application layer

## Unit tests
The app has 86% of unit tests code coverage. 
(tool user for code coverage: dotCover)

## Check out the app [here](http://stsenova-001-site1.atempurl.com/)
