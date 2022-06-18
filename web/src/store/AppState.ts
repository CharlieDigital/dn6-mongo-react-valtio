import { proxy } from "valtio";
import {
  CompanyService,
  Employee,
  EmployeeService,
  NotifcationsService,
} from "../services";
import { Company } from "../services/models/Company";
import { companyNames, firstNames, lastNames, titles } from "./RandomData";
import { signalRService } from "./SignalRService";

class AppState {
  public companies: Company[] = [];

  public authenticated: boolean = false;

  /**
   * Broadcast a message using SignalR.
   */
  public async broadcast() {
    var message = "Howdy";
    NotifcationsService.echo({ message });
  }

  /**
   * Add a random company via API call and then adds the instance into the array.
   */
  public async addRandomCompany() {
    // If we have all 25, we can't add MORE.
    if (appState.companies.length === companyNames.length) {
      return;
    }

    let randomName: string;

    do {
      randomName =
        companyNames[Math.floor(Math.random() * companyNames.length)];
    } while (
      appState.companies.findIndex((c) => c.label === randomName) > -1 // If we find the name, try another.
    );

    let response = await CompanyService.addCompany({
      requestBody: {
        id: "",
        label: randomName,
        address: null,
        webUrl: null,
      },
    });

    appState.companies.push(response);
  }

  /**
   * Deletes a company by ID.
   * @param company The company to delete.
   */
  public async deleteCompany(company: Company) {
    let response = await CompanyService.deleteCompany({ id: company.id || "" });

    // console.log(response);
    // {deletedCount: 1, isAcknowledged: true}

    const index = appState.companies.findIndex((c) => c.id === company.id);

    appState.companies.splice(index, 1);
  }

  /**
   * Performs the load of Companies from the back end.
   */
  public async loadCompanies() {
    let response = await CompanyService.getAllCompanies({ start: 0 });

    appState.companies.splice(0, 0, ...response);
  }

  /**
   * Adds a randomly generated Employee to a Company
   * @param company The Company to add the Employee to.
   */
  public async addEmployeeTo(company: Company) {
    const firstName = firstNames[Math.floor(Math.random() * firstNames.length)];
    const lastName = lastNames[Math.floor(Math.random() * lastNames.length)];
    const title = titles[Math.floor(Math.random() * titles.length)];
    const salary = Math.floor(Math.random() * (250000 - 125000) + 125000);

    const employee: Employee = {
      id: "",
      label: title,
      firstName: firstName,
      lastName: lastName,
      salary: salary,
      company: {
        id: company.id,
        label: company.label,
        collection: "Company",
      },
    };

    const newEmployee = await EmployeeService.addEmployee({
      requestBody: employee,
    });

    appState.companies
      .find((c) => c.id === company.id)
      ?.employees?.push(newEmployee);
  }

  /**
   * Loads the Employees for a given Company
   * @param company The Company to load the Employees for.
   */
  public async loadEmployeesFor(company: Company) {
    let response = await EmployeeService.getByCompany({
      companyId: company.id || "",
      start: 0,
    });

    appState.companies
      .find((c) => c.id === company.id)
      ?.employees?.splice(0, 0, ...response);
  }

  /**
   * Computed getter which returns the total compensation of all employees.
   */
  public get totalCompensation(): Number {
    let total = 0;

    appState.companies.forEach((c) => {
      c.employees?.forEach((e) => {
        total += e.salary || 0;
      });
    });

    console.log("Calculating total compensation...");

    return total;
  }
}

export const appState = proxy(new AppState());
