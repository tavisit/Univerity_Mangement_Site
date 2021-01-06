# Univerity_Mangement_Site

## Table of contents
* [Technologies](#technologies)
* [General info](#general-info)
* [To do List](#to-do-list)

## Technologies
* [C# V7.3](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [ASP.NET V4.7.2](https://dotnet.microsoft.com/)
* [Entity Framework V6.0.0.0](https://docs.microsoft.com/en-us/ef/) 
* [HTML Agility Pack V1.11.29.0](https://github.com/zzzprojects/html-agility-pack)
* [SQL SERVER V15.0.2070.41](https://support.microsoft.com/en-us/help/4518398/kb4518398-sql-server-2019-build-versions)

## General info

### What is it?

This project is an web-based university management system as a result of an project assignment for the Database Course as part of the mandatory curriculum of Automatics and Computers Faculty of Technical University of Cluj-Napoca. 

The inspiration came from a similar system actually deployed to fit my university needs, but some key features in the backend were missing, so I tried to implement a basic functionality of them.

This is a demo and shall be treated as one, as I don't own any copyright on the images I used and information stored in the database.

### Features

In this section there will be presented the key features of the system developed

#### 4 types of accounts: Student, Teacher, Worker and Admin with various privileges and features

* All accounts have the posibility to change the details of the account or delete it
* The Student's account can see his/her grades and their collegues in the series. Dorm information and payment available if the student applies to be in the dorm;
* The Teacher's account has a table with all the students assigned to their course, with the posbility of grading them and, also, some general information about the teacher's status, Phd info etc;
* The Worker account needs to be implemented;
* The admin has the ability to register teachers and a table with all the accounts existing in the database

#### Additional features

* The home page has information scrapped using HTML Agility Pack, including announcements from the Automatics and Computers Faculty website and news from the TUCN website
* Registration possible only for the student type, the Teacher/Worker account will be added by Admin
* Dorm payments automatically updated every month
* For every user, only the ID is visible, in order to garantee the application of the European GDPR laws and rules regarding the personal information usage.
* Custom profile for every account accessable from student's collegues section and their teacher
* Fake payment of the dorm, its value increasing each month

## To do List

* Encryption of the Database
* Worker account
* Beautify the website
* Custom announcements to the student/teacher faculty
