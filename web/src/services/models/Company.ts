/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

/**
 * Models a Company entity.
 */
export type Company = {
    id?: string | null;
    label?: string | null;
    /**
     * The address of the company.
     */
    address?: string | null;
    /**
     * The URL of the website for the given company.
     */
    webUrl?: string | null;
}