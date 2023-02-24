## Deployment and Network Registration Details
This repository contains a reference implementation of the BPP that has been onboarded on the Beckn Gateway and Beckn Gateway Registry under the Jobs and Internships category.
- [BPP Deployed URL](https://6vs8xnx5i7.execute-api.ap-south-1.amazonaws.com/dsep) [swaggger](https://6vs8xnx5i7.execute-api.ap-south-1.amazonaws.com/dsep/swagger/index.html)
- BAP [use postman collection to test the BPP](https://github.com/beckn/DSEP-Specification/blob/master/artefacts/postman-collections/jobs-internships/jobs-internships-postman-collection.json)
- BPP Network Participant Id: [affinidi.com.bpp](https://github.com/affinidi/reference-api-dsep-jobs-bpp)
- [Network Participant Information on Beckn Registry](https://registry.becknprotocol.io/network_participants/search/Affinidi/network_participants/show/359)


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
