# 99x-assessment2

DemoUrl: https://99xassessment220220924210846.azurewebsites.net/

This contains 2 projects in it i.e.. 99xAssessment2 (The WebApp) & 99xAssessment2.Test (Unit tests)

The web app can works with 2 types of roles:
1. SuperAdmin: This role can do excel upload & can also sees the report.
2. Admin: This role is just a normal user, who can login and can sees the reports only.

Set up SuperAdmin & Admin user(s):-
This can be done once the application able to run on localhost.
Just execute the below url's to create users:-
Ex: 
For AdminUser: https://99xassessment220220924210846.azurewebsites.net/account/AddAdminUser?userName=user1@demo.com
For SuperAdminUser: https://99xassessment220220924210846.azurewebsites.net/account/AddSuperAdminUser?userName=superadmin1@demo.com

Password will be taken from the configured key from web.config (<add key="SuperAdminPwd" value="Simran1990-=" />)

Before buidling:
1. Setup/Modify connection string in web.config as per your SQL Conn.
2. Adjust the password in this key (<add key="SuperAdminPwd" value="Simran1990-=" />)
3. Build solution.

Unit test:
Unit test is testing mainly the repo i.e.. DAL.
Unit test is also testing the excel & other nvalid type upload, which is taken from the sample folder i.e.. 99xAssessment2.Tests\ExcelSample

