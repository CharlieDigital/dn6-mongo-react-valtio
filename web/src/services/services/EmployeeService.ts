/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { DeleteResult } from '../models/DeleteResult';
import type { Employee } from '../models/Employee';
import type { CancelablePromise } from '../core/CancelablePromise';
import { request as __request } from '../core/request';

export class EmployeeService {

    /**
     * Adds a new Employee to the database.  Set the ID to the empty string ""
 * and a new ID will be assigned automatically.  The returned entity will
 * have the new ID.
     * @returns Employee Success
     * @throws ApiError
     */
    public static addEmployee({
requestBody,
}: {
/** The Employee instance to add. **/
requestBody?: Employee,
}): CancelablePromise<Employee> {
        return __request({
            method: 'POST',
            path: `/api/employee/add`,
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * Deletes a Employee given an ID.
     * @returns DeleteResult Success
     * @throws ApiError
     */
    public static deleteEmployee({
id,
}: {
id: string,
}): CancelablePromise<DeleteResult> {
        return __request({
            method: 'DELETE',
            path: `/api/employee/delete/${id}`,
        });
    }

    /**
     * Gets a Employee by ID
     * @returns Employee Success
     * @throws ApiError
     */
    public static getEmployee({
id,
}: {
/** The ID of the Employee to retrieve. **/
id: string,
}): CancelablePromise<Employee> {
        return __request({
            method: 'GET',
            path: `/api/employee/${id}`,
        });
    }

    /**
     * Gets the list of Employees by the company ID.
     * @returns Employee Success
     * @throws ApiError
     */
    public static getByCompany({
companyId,
start,
pageSize = 25,
}: {
/** The ID of the company to retrieve employees for. **/
companyId: string,
/** The starting index of companies to retrieve. **/
start: number,
/** The number of companies to retrieve. **/
pageSize?: number,
}): CancelablePromise<Array<Employee>> {
        return __request({
            method: 'GET',
            path: `/api/employee/company/${companyId}/${start}/${pageSize}`,
        });
    }

}