import * as pulumi from "@pulumi/pulumi";
import * as gcp from "@pulumi/gcp";

//======================================================================================
// Create a GCP Storage Bucket that will serve the application
// We'll push the application from GitHub Actions
const bucket = new gcp.storage.Bucket("dn6-mongo-web-pulumi", {
    location: "US",
    website: {
        mainPageSuffix: "index.html"
    },
    uniformBucketLevelAccess: true
});

// Create the binding required to allow public access
const bucketIAMBinding = new gcp.storage.BucketIAMBinding("dn6-mongo-web-pulumi-iam", {
    bucket: bucket.name,
    role: "roles/storage.objectViewer",
    members: ["allUsers"]
});

// Export the full endpoint name of the bucket
export const bucketEndpoint = pulumi.concat("http://storage.googleapis.com/", bucket.name);

// Export the DNS name of the bucket
export const bucketName = bucket.url;

//======================================================================================
// TODO: Create the GCP Cloud Run assets; depending on how you want to operationalize
// The options are to:
//  - Create it using Google Cloud Build to create the image
//  - Create it using a build pipeline to create the Docker image and push to Cloud Artifacts
//  - Create it using a Google Cloud Repository mirror of GitHub with a Build Trigger

//======================================================================================
// TODO: Create the GCP Scheduler asset that resets the project