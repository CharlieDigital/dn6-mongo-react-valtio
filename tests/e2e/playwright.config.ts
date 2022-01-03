import { PlaywrightTestConfig, devices } from "@playwright/test";

// See: https://playwright.dev/docs/intro#installation
const config: PlaywrightTestConfig =
{
    forbidOnly: !!process.env.CI,
    retries: process.env.CI ? 2 : 0,
    use:
    {
        trace: 'on-first-retry',
        headless: false,
        baseURL: 'http://localhost:3000',
        actionTimeout: 5000, // Consider an error if the action can't complete in 5 seconds.
        navigationTimeout: 5000 // Consider an error if the navigation doesn't occur in 5 seconds.
    },
    // See: https://playwright.dev/docs/test-reporters
    reporter:
    [
        ['list'],
        ['json', { outputFile: './test_results/results.json' }],
        ['junit', { outputFile: './test_results/results.xml' }],
        ['html', { outputFolder: './test_results/report' }]
    ],
    projects:
    [
        {
            name: 'chromium',
            use: { ...devices['Desktop Chrome'] },
        },
        /*
        {
            name: 'firefox',
            use: { ...devices['Desktop Firefox'] },
        },
        {
            name: 'webkit',
            use: { ...devices['Desktop Safari'] },
        },
        */
    ],
};

export default config;