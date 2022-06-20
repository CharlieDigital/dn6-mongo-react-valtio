/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { request as __request } from '../core/request';

export class NotifcationsService {

    /**
     * Echos a message to all connected clients.
     * @returns any Success
     * @throws ApiError
     */
    public static echo({
        message,
    }: {
        /** The message to send to all connected clients.. **/
        message: string,
    }): CancelablePromise<any> {
        return __request({
            method: 'GET',
            path: `/api/notifications/echo/${message}`,
        });
    }

    /**
     * Joins the specified connection ID to the group.  The group name is arbitrary and could represent
     * any scope; it can be an ID, a location, etc.
     * @returns any Success
     * @throws ApiError
     */
    public static joinGroup({
        connectionId,
        group,
    }: {
        /** The ID of the connection to join to the group. **/
        connectionId: string,
        /** The name of the group to join the connection to. **/
        group: string,
    }): CancelablePromise<any> {
        return __request({
            method: 'GET',
            path: `/api/notifications/join/${connectionId}/${group}`,
        });
    }

    /**
     * Notifies the taret group with a message.
     * @returns any Success
     * @throws ApiError
     */
    public static notifyGroup({
        group,
        message,
    }: {
        /** The name of the group to send the message to. **/
        group: string,
        /** The message to broadcast to the group. **/
        message: string,
    }): CancelablePromise<any> {
        return __request({
            method: 'GET',
            path: `/api/notifications/notify/${group}/${message}`,
        });
    }

}