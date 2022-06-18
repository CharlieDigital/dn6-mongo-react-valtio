/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { request as __request } from '../core/request';

export class NotifcationsService {

    /**
     * Gets a Employee by ID
     * @returns any Success
     * @throws ApiError
     */
    public static echo({
        message,
    }: {
        message: string,
    }): CancelablePromise<any> {
        return __request({
            method: 'GET',
            path: `/api/notifications/echo/${message}`,
        });
    }

}