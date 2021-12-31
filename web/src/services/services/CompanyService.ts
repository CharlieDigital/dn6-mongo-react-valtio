/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Company } from '../models/Company';
import type { CancelablePromise } from '../core/CancelablePromise';
import { request as __request } from '../core/request';

export class CompanyService {

    /**
     * Adds a new company to the database.
     * @returns any Success
     * @throws ApiError
     */
    public static addCompany({
requestBody,
}: {
/** The company instance to add. **/
requestBody?: Company,
}): CancelablePromise<any> {
        return __request({
            method: 'POST',
            path: `/api/company/add`,
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * Deletes a Company given an ID.
     * @returns any Success
     * @throws ApiError
     */
    public static deleteCompany({
id,
}: {
id: string,
}): CancelablePromise<any> {
        return __request({
            method: 'DELETE',
            path: `/api/company/delete/${id}`,
        });
    }

    /**
     * Gets a Company by ID
     * @returns any Success
     * @throws ApiError
     */
    public static getCompany({
id,
}: {
/** The ID of the company to retrieve. **/
id: string,
}): CancelablePromise<any> {
        return __request({
            method: 'GET',
            path: `/api/company/${id}`,
        });
    }

}