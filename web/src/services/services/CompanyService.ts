/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Company } from '../models/Company';
import type { DeleteResult } from '../models/DeleteResult';
import type { CancelablePromise } from '../core/CancelablePromise';
import { request as __request } from '../core/request';

export class CompanyService {

    /**
     * Gets the list of companies matching a specific page sorted by the title.
     * @returns Company Success
     * @throws ApiError
     */
    public static getAllCompanies({
        start,
        pageSize = 25,
    }: {
        /** The starting index of companies to retrieve.  Optional; 0 if not specified. **/
        start: number,
        /** The number of entries to retrieve.  Optional; 25 if not specified. **/
        pageSize?: number,
    }): CancelablePromise<Array<Company>> {
        return __request({
            method: 'GET',
            path: `/api/company/list/${start}/${pageSize}`,
        });
    }

    /**
     * Adds a new company to the database.  Set the ID to the empty string ""
     * and a new ID will be assigned automatically.  The returned entity will
     * have the new ID.
     * @returns Company Success
     * @throws ApiError
     */
    public static addCompany({
        requestBody,
    }: {
        /** The company instance to add. **/
        requestBody?: Company,
    }): CancelablePromise<Company> {
        return __request({
            method: 'POST',
            path: `/api/company/add`,
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * Deletes a Company given an ID.  Deletes all Employees that reference the
     * Company as well.
     * @returns DeleteResult Success
     * @throws ApiError
     */
    public static deleteCompany({
        id,
    }: {
        id: string,
    }): CancelablePromise<DeleteResult> {
        return __request({
            method: 'DELETE',
            path: `/api/company/delete/${id}`,
        });
    }

    /**
     * Gets a Company by ID
     * @returns Company Success
     * @throws ApiError
     */
    public static getCompany({
        id,
        full = false,
    }: {
        /** The ID of the company to retrieve. **/
        id: string,
        /** When specified, returns the rich object **/
        full?: boolean,
    }): CancelablePromise<Company> {
        return __request({
            method: 'GET',
            path: `/api/company/${id}/${full}`,
        });
    }

}