import { test, expect } from '@playwright/test';

test('Create and delete Company', async({ page }) => {
    await page.goto('http://localhost:3000');

    let locator = page.locator('text=/Showing 0 companies/');
    await expect(locator).toBeVisible();

    await page.click('"Add Company"');

    locator = page.locator('text=/Showing 1 companies/');
    await expect(locator).toBeVisible();

    await page.click('[data-testid="DeleteIcon"]');

    locator = page.locator('text=/Showing 0 companies/');
    await expect(locator).toBeVisible();
});