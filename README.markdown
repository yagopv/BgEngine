# What is BgEngine

BgEngine is a blog engine build using ASP MVC 3, jQuery and Entity Framework Code First.

This engine allow users to create blog posts, image galleries and video galleries.

No database server needed because the application is using a Compact Framework database.

# Introduction

BgEngine is an open source application licensed under GPLv3 that builds an engine for personal blogging purposes.

## Scope

The blog engine developed allow users publishing articles,  building photographic galleries, creating a video gallery and share all the posts in the social networks more highlighted. The main scope is using the application for blogging, mainly like personal website and provide the functionality of this type of web sites.

# Technologies and  features

Main list of technologies used in this project are:

    ASP MVC 3  (Web)
    jquery (client side scripting)
    jquery ui (user interface)
    Entity Framework 4.3.1  'Code First' (ORM for data access)
    Structure Map  (dependency injection)
    Combres (combining, compressing and minifying script and css files) 

The default database is a .sdf Compact Framework database, but is easy to change to SQL Server database simply changing the web.config file and uncomment the database initializer lines in Global.asax. 

The Web site built in this project have two main parts:

## FrontEnd

* Home page listing the latest articles and videos being published
* Image Gallery listing albums in the site
* Video Gallery listing videos linked from the site
* About me page for include information about the site
* Creating new user accounts is possible in the frontend web site. Confirmation mail will be sent when users creates an account.
* Email button in footer opening popup for sending messages to the  email account configured in the backend

* Sidebar widgets including.  

    * Social bar. Could share pages in Facebook, Twitter and Google+ and subscribe to the rss feed showing latest articles
    
    * Category widget showing categories in the site with number of related articles in each category. Each category links to another page showing articles in the category
    
    * Archive widget showing articles published by month. Each month links to another page showing articles published in that month - year
    
    * Stats Widget showing information about articles like, more visits, more comments and top rated articles
    
    * Tag cloud showing the tags and number of articles tagged with it. Each tag links to another page showing articles published marked with that tag
	
	* Subscription to newsletter
    
    * Twitter Profile widget. Shows Twitter profile if configured in the backend
    
    * Twitter Search widget. Shows tweets following the search previously configured in the backend. Typically you are configuring it with a search word like @myusername for watching who is talking about you
    
    * Displays posts lists
    
    * Displays individual posts and allows rating them or writing comments in a anonymous or authenticated way (Admin users can select in the backend  this behaviour)

## BackEnd

* Management of the information displayed in frontend
* Management image galleries
* Management video galleries
* Management of subscriptions and newsletters
* Upload files to the server
* Display stats from the site
* Configuration of the site (Mail account, paging, Twitter, Google analytics ...)