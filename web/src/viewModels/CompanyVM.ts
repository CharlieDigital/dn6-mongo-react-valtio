import { Company } from "../services";
import { ViewModelBase } from "./ViewModelBase";

/**
 * View model class that implements Company and extends it.
 */
export class CompanyVM extends ViewModelBase<Company>
{
    /**
     * Gets the total compensation for the company.
     */
    public get totalCompensation() : Number
    {
        let sum = 0;

        this.model.employees?.forEach(e =>
        {
            sum += e.salary || 0;
        });

        return sum;
    }
}


