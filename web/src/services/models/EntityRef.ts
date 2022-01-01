/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

/**
 * Represents an entity reference from one entity to another.  It inherits
 * the base properties of ID and Label from the base entities which allows
 * for efficient display of the relationship in the UI.
 */
export type EntityRef = {
    /**
     * The ID of the entity in the MongoDB
     */
    id?: string | null;
    /**
     * The name associated with the entity in the MongoDB
     */
    label?: string | null;
    /**
     * The collection that the entity exists in.
     */
    collection?: string | null;
}