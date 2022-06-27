# PRC.PacketBatchFiller

## The essence of my story

The essence of work in one of my positions was to check the documents for the correctness of their completion. However, the documents were all filled out manually, by simply entering data into an MS Word file. Document flow included more than 30 different forms. For each client, it was required to fill in from 5 to 30 documents. The documents contained repetitive data, such as last name, first name, ID data, and so on. Filling out and checking 150+ documents a day is a laborious job, which was done by 5 employees. Any error in the document was the reason for refusing to provide the service to the client.

I started development from scratch, in the absence of any knowledge and experience in programming. I came to work a couple of hours before the start and stayed for several hours after work, as well as within the working day in my free time from the main tasks - all for the sake of development about 1.5 years passed from the time the development began to the program was put into operation.
As a result, I made an application that significantly reorganized the work of the entire enterprise.

## So, about the application:
* It is a Windows desktop .NET application, using WPF and C# with the Model-View-ViewModel pattern;
* Organized data layer using entity framework with code-first approach;
* All data entered by the operator is strictly verified and standardized: from a simple calculation of the validity of a passport to the use of various databases of third parties for filling and structuring addresses, TINs, bank information and the like (thanks to the dadata.ru service);
* The entire history of customer requests is collected in a single Microsoft SQL database, where all actions related to the client are stored;
* Well-designed, user-friendly application interface, with the ability to enter data without using the mouse;
* All fields of documents, the filling of which can later be reused, are converted into a reference book;
* Organized a mechanism for interaction with office telephony: starting a call with one click and opening a client card with an incoming call;

## What is the result?
* The organization, in fact, received a self-written customer relationship management system.
* The number of errors in the preparation of documents decreased by 30 times.
* The time to receive new customers was reduced by 8 times.
* The entire history of client requests is now collected in a single database and the speed of generating documents from clients in the event of a repeated request becomes instantaneous.
* The staff of the organization in all branches of the country, which were engaged in receiving clients and processing documents, decreased by 4 times (Oops… 🙈)
* The application is still in use (4 years) and, surprisingly, it works stably and does not require any maintenance.

However, in 2018, friends offered me that I open my own meat trading business, and my potential as a developer remained unfulfilled. But the war unleashed by Putin changed everything. Now I am in Israel, a country of high technologies, and I want to get into software development because I have a passion for this work. I will try to do everything necessary to ensure that my dream this time does not remain just a dream.

At the moment I am actively looking for a job or internship, I enjoy learning Java, spring framework, git, docker, setting up a server on AWS EC2 with Ubuntu, setting up PostgreSQL, wrote a couple of hello-world applications, one of which performs basic CRUD operations in the database with accessing the corresponding API methods, and the second is a Telegram bot that communicates with Telegram via webhooks.

Perhaps you have a more exciting task to hone my skills? I’m open to any offers! 😊

