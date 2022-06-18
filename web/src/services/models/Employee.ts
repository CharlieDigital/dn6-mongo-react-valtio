/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { EntityRef } from './EntityRef';

/**
 * Core data model for an employee.
 */
export type Employee = {
    /**
     * The ID of the entity in the MongoDB
     */
    id?: string | null;
    /**
     * The name associated with the entity in the MongoDB
     */
    label?: string | null;
    /**
     * The first name of the employee.
     */
    firstName?: string | null;
    /**
     * The last name of the employee.
     */
    lastName?: string | null;
    /**
     * The salary assigned to the employee.
     */
    salary?: number;
    company?: EntityRef;
}
