/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Employee } from './Employee';

/**
 * Models a Company entity.
 */
export type Company = {
    /**
     * The ID of the entity in the MongoDB
     */
    id?: string | null;
    /**
     * The name associated with the entity in the MongoDB
     */
    label?: string | null;
    /**
     * The address of the company.
     */
    address?: string | null;
    /**
     * The URL of the website for the given company.
     */
    webUrl?: string | null;
    /**
     * The list of Employees associated with this Company.
     */
    employees?: Array<Employee> | null;
}
