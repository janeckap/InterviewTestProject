Solution contains:
- Original Backend project and Angular frontend project

Backend - InterviewTestProject.TranslationManagement:
- Built using .NET 7
- Can be run using the command: `dotnet run --project TranslationManagement.Api`
- Code refactored into five separate projects:
  - Api: Contains the Program, Controllers (moderate focus to make the API restful), and DTOs.
  - Application: Application layer, contains services (File handling, notification wrapper, app logic), repositories.
  - Data: Data layer, contains the DB Context and migrations.
  - Domain: Contains domain models and enums.
  - Tests : Contains unit tests.
- What could be improved further:
  - Add more DTOs to separate models from DTOs.
  - Introduce mapping using a library like AutoMapper.
  - Create a connection between translators and jobs. (Not clear if it's a one-to-many or many-to-many relationship).
  - Implement more endpoints, tests, CORS, etc.

Frontend - InterviewTestProject.FrontEnd:
- Built using Angular. Requires Node.js and Angular to be installed. Angular can be installed using the command: `npm install -g @angular/cli`
- Project can be run using the commands: `npm install` and `ng serve`
- Implementation is quite basic as per requirements. Upon loading, a list of translators appears. Users can create a new translator and edit their status.
- Project consists of a few components under `src/app/translator`: `translator-list`, `create-translator`, and `update-status`.