/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { request as __request } from '../core/request';

export class AdminService {

    /**
     * Resets the environment by dropping the collections.
     * @returns any Success
     * @throws ApiError
     */
    public static resetEnv(): CancelablePromise<any> {
        return __request({
            method: 'DELETE',
            path: `/api/admin/reset`,
        });
    }

}