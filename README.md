# ClaimReversingAPI using BuilderPattern

![image](https://user-images.githubusercontent.com/51775632/173429269-ec8fe5d3-374c-4be6-ae2e-ea16aca5c45d.png)

In the above GitHub repository, you will find interfaces for the IClaimBuilder and the IClaimDirector. Many resources advise you to use abstract base classes for the director and the builder - which is fine. I simply like interfaces more, so I went this route. The IClaimbuilder interface contains the definition for building the claim and retrieving the claim instance. The IClaimDirector simply has a construct method and builds a claim. In this method, all steps should be done to build a specific thing - in our case a given type of operation to process data. The Input class stores the model of a product. To make it simple I minimized the claim to the product with getter and setter. To set a value, you have to use the constructor when creating a claim.

Claim Builder class hold a sequence of operation requested by the director and each carries a single responsibility. It has reset.

# Data Pipeline 

![Blank diagram](https://user-images.githubusercontent.com/51775632/173450470-6f864ed0-a2e9-4433-9922-94e88a8df7cd.png)


# Project Structure 

Abstract_Layer 

   > Interface - Interface segregation
 
   > Repository - Business Logic
 
   > Services  - Builder Pattern settings 
 
ClaimReversingAPI - API layer

Data_Model - Entity

Claim_Reversing_Test - Unit Test


# Build and Run

Prerequisite
 - Visual Studio 19 +
 - .Net Core 3.1 +
 - C# 8.0 +

Run on local
 - Setup / download git repository
 - Build 
 - Run - IIS / Docker
 - Result screen 
 <img width="871" alt="image" src="https://user-images.githubusercontent.com/51775632/173432405-3733d4e7-0c57-4800-9f11-55457f85f182.png">

 - Upload input file  : https://github.com/m2sachinpatil/BuilderPattern-ClaimReversingAPI/blob/main/inputFile.txt 
 - Execute 
 - Result Screen
 <img width="616" alt="image" src="https://user-images.githubusercontent.com/51775632/173432561-27108aff-f828-467a-9ab9-55facac0dc05.png">


# Future Scope to enhance
- Instead of data table structure to process data, we can use the Map of the vector of structure for data load and aggregate. Which helps to improve data mapping usng auto mapper.
- Unit test is written for demo purposes. Need more for coverage.
 

