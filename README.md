<div align="center">

# Decentralised Skilling and Education Protocol: Jobs and Internships

</div>

<div>
Decentralized Skilling and Education Protocol project provides open interoperable specifcations for creating decentralized skills and education networks. It is an adaptation of beckn protocol core specification with added taxonomies and sample network policies for the skills and education sector.


This document serves as a reference for the implementation of the Beckn Provider Platform (BPP) specifically tailored to the Jobs and Internships track, compliant with the DSEP spec v1.0.0. The Job and Internship BPP Service facilitates interactions between job providers/employers and job seekers, with the aim of establishing a transparent ecosystem that promotes learning, connection, problem-solving, and knowledge-sharing within communities.
  </div>

## Deployment and Network Registration Details
This repository contains a reference implementation of the BPP that has been onboarded on the Beckn Gateway and Beckn Gateway Registry under the Jobs and Internships category.
-  Domain
 > dsep:jobs
- [BPP Deployed URL](https://6vs8xnx5i7.execute-api.ap-south-1.amazonaws.com/dsep) [swaggger](https://6vs8xnx5i7.execute-api.ap-south-1.amazonaws.com/dsep/swagger/index.html)
- BAP [use postman collection to test the BPP](https://github.com/beckn/DSEP-Specification/blob/master/artefacts/postman-collections/jobs-internships/jobs-internships-postman-collection.json)
- BPP Network Participant Id: [affinidi.com.bpp](https://github.com/affinidi/reference-api-dsep-jobs-bpp)
- [Network Participant Information on Beckn Registry](https://registry.becknprotocol.io/network_participants/search/Affinidi/network_participants/show/359)


## Architecture

  <img width="1188" alt="image" src="https://user-images.githubusercontent.com/125359926/221154141-80eef096-80e8-40d1-87f2-0d94c9973dcb.png">

## Supported methods 

- /search: 
> This endpoint enables users to search for jobs and internships by sending a direct DSEP-compliant request to the BPP with the context.domain set as dsep:jobs. This allows for a targeted and efficient search experience for users.
- /select: 
> This endpoint enables users to select a specific job or internship and retrieve more detailed information about it. The context domain for this method should be set as dsep:jobs. In the reference implementation of the course discovery platform, this endpoint is triggered when a user expands a particular job to view its details.
- /init: 
> This endpoint allows for initiating application for job or internship by getting a validation from the BPP for posted Xinput data. The context domain for this method should be dsep:jobs.
- /confirm: 
> This endpoint confirms the submission of an application after successful validation during the Init request.
- /status:
> This endpoint allows users to retrieve the status of their job application. The application status may change over time, starting from the initial screening phase, progressing to either Accepted or Rejected, and ultimately, if selected, leading to the Onboarding phase. 

#### Each of these API calls requires a BAP URL to send data back to the BAP. Therefore, the client (BAP) must expose the on_search, on_select, on_init, on_confirm, and on_status endpoints to receive the response from the BPP. These endpoints should be configured to receive the appropriate data format and handle the responses accordingly.



## Tech stack

-   .Net 6.0 
-   OpenSearch 2.4.0


## Prerequisite to run the BPP API

-   An API to query the job catalogue : in this example DAL-API (Data access layer- API ) 
-   Environment variables as below  

  --

    searchUrl
    searchbaseUrl
    save_xinput_url
    bpp_privatekey
    bpp_subscriber_id
    bpp_unique_key_id
    bpp_url
    bpp_Xinput_url
    verify_signature
    dsep_registry_url
    verify_proxy_signature
    
## Prerequisite to run the DAL-API

- ANY document DB with rest API interface : this example uses OpenSearch to query the catalogue

## Setup Options

Job and Internship BPP service can be setup in local using docker images:

<summary>As part of multi-container local setup with all other services and dependencies using Docker-Compose</summary>

### Docker-Compose Setup Guide
#### what you need 
- docker run time
- docker compose
- postman(or similar )

- #### components in deployment: 

  - BPP
  - Data access layer API 
  - OpenSearch 

  
#### building docker  image for service 
- BPP 

  Navigate to BPP folder 

> docker build . -t dsep-bpp

- Data access layer API  
  
  Navigate to DAL-API 
  
 > docker build . -t dal-api

 Then use the local docker compose file https://github.com/sanjay95/reference_DSEP_BPP/blob/main/docker-compose-local.yml
to start the containers

> docker-compose - f < docker-compose-local.yml file location > up -d 

> If you do not want to build the images and use the available images to run the setup, use the default docker compose file 
https://github.com/sanjay95/reference_DSEP_BPP/blob/main/docker-compose.yml
to start the containers 

#### BPP APIs will be available at 
- http://localhost:8088/search
- http://localhost:8088/select
- http://localhost:8088/confirm
- http://localhost:8088/status

##### Job Portal APIs will be available at 
http://localhost:8080/opensearch


- Create a job 
  - POST
  - http://localhost:8080/opensearch/addjob
    Payload sample:   https://github.com/sanjay95/reference_DSEP_BPP/blob/main/Sample_Job.json
    Job ID can be null, it will be generated by API.
- Get a single job for  ID
  - GET
  - http://localhost:8080/opensearch/jobs/AC9449C7608C944F5159B0C9FAEAAED0
- Get all applications for a job ID
  - GET
  - http://localhost:8080/opensearch/applications?jobid=8A690F158A37965A2CC0E69EC8B360B2
- Get A single application details
  - GET
  - http://localhost:8080/opensearch/applications/1676379236106
- Update an applications status
  - POST
  - http://localhost:8080/opensearch//applications

###### Application sample payload 
https://github.com/sanjay95/reference_DSEP_BPP/blob/main/Application_sample.json







