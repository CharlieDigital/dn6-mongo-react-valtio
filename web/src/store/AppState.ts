import { proxy } from "valtio";
import { CompanyService } from "../services";
import { Company } from "../services/models/Company";

const companyNames: string[] = [
    "Cummins",
    "Williams",
    "VF",
    "Universal Health Services",
    "Fiserv",
    "PulteGroup",
    "NGL Energy Partners",
    "PNC Financial Services",
    "Coca-Cola",
    "Cheniere Energy",
    "Eastman Chemical",
    "JetBlue Airways",
    "Yum Brands",
    "Interpublic Group",
    "Northrop Grumman",
    "National Oilwell Varco",
    "Peabody Energy",
    "Westlake Chemical",
    "Reinsurance Group of America",
    "Anadarko Petroleum",
    "Penske Automotive Group",
    "PPG Industries",
    "Abbott Laboratories",
    "Hertz Global Holdings",
    "Eversource Energy"
]

class AppState
{
    public companies: Company[] = [];

    /**
     * Add a random company via API call and then adds the instance into the array.
     */
    public async addRandomCompany()
    {
        // If we have all 25, we can't add MORE.
        if (appState.companies.length === companyNames.length)
        {
            return;
        }

        let randomName: string;

        do {
            randomName = companyNames[Math.floor(Math.random() * companyNames.length)];
        } while (
            appState.companies.findIndex(c => c.label === randomName) > -1 // If we find the name, try another.
        );

        let response = await CompanyService.addCompany(
            {
                requestBody:
                {
                    id: "",
                    label: randomName,
                    address: null,
                    webUrl: null
                }
            }) as Company;

        appState.companies.push(response);
    }

    /**
     * Deletes a company by ID.
     * @param company The company to delete.
     */
    public async deleteCompany(company: Company)
    {
        let response = await CompanyService.deleteCompany({ id : company.id || '' });

        // console.log(response);
        // {deletedCount: 1, isAcknowledged: true}

        const index = appState.companies.findIndex(c => c.id === company.id);

        appState.companies.splice(index, 1);
    }
}

export const appState = proxy(new AppState());