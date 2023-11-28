# Project_3-Quiz

## General Info
School assignment to build an application where users can create, share and take each others quizzes. 
The app was built using Blazor Webassembly and a ASP.NET API in .NET 7.
Azure SQL was used for the database along with entity framework.

## Required functionality
User is able to create a quiz.
A quiz is comprised of a title, a timelimit for it's completion and a number of questions
Each question can have an image or video attached.
Answer to the question can either be multiple choice or free text.
It should be possible to link directly to the quiz.
Users can see a list of the quizzes they have created and which other users have responded to their quizzes and a leadeboard.
Users should be able to respond to a quiz they have been given a link to.

## Design
A repository pattern was used to establish an abstraction of the data layer. The database design of the application is as seen below. 

<img align="right" src="https://i.ibb.co/xMsfDLY/Quiz-app-entity.png" alt="image of main menu" width="600"/>

## How to play
Run the update database command after downloading the project.
Adjust connection string to match your settings.
Build the solution (remember to start both API and Client).
Register a user and create a quiz!
