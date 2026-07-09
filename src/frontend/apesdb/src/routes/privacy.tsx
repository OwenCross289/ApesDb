import { Link, createFileRoute } from "@tanstack/react-router";
import { appName } from "@apesdb/common";

const effectiveDate = "10 July 2026";
const hostingProvider = "self-hosted infrastructure in Norway";
const storageCountry = "Norway, with authentication data stored by Auth0 in the EU";
const logRetentionDays = "90";
const contactEmail = "jb8fqjxoy@mozmail.com";
const contactName = "Apes";

export const Route = createFileRoute("/privacy")({
  component: PrivacyPolicy,
});

function PrivacyPolicy() {
  return (
    <main className="flex min-h-svh flex-col bg-background px-6 py-12 text-foreground">
      <div className="mx-auto w-full max-w-3xl space-y-8">
        <div className="space-y-2">
          <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
            {appName}
          </p>
          <h1 className="text-3xl font-semibold tracking-tight">Privacy Policy</h1>
          <p className="text-sm text-muted-foreground">Effective date: {effectiveDate}</p>
        </div>

        <section className="space-y-3">
          <p className="text-muted-foreground">
            {appName} is a private website used by a small friend group to organise co-op gaming
            sessions and compare Steam game libraries.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Who can use this site</h2>
          <p className="text-muted-foreground">
            Access to this site is restricted to approved Google accounts on our allowlist. It is
            not intended for public use.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Information we collect</h2>
          <p className="text-muted-foreground">
            We collect only the information needed to run the site:
          </p>
          <ul className="list-disc space-y-2 pl-5 text-muted-foreground">
            <li>
              <span className="font-medium text-foreground">Google sign-in information:</span> your
              Google email address and basic profile information, used to sign you in and check
              whether you are on the allowlist.
            </li>
            <li>
              <span className="font-medium text-foreground">Steam information:</span> your Steam ID,
              Steam display/profile information, and Steam game library information retrieved
              through the Steam Web API, such as owned games, app IDs, game names, and playtime
              where available.
            </li>
            <li>
              <span className="font-medium text-foreground">Session information:</span> any co-op
              sessions, availability, RSVPs, notes, or preferences you add to the site.
            </li>
            <li>
              <span className="font-medium text-foreground">Technical information:</span> basic
              server logs, such as IP address, browser information, request times, and error logs,
              used for security and debugging.
            </li>
          </ul>
          <p className="text-muted-foreground">
            We do <span className="font-medium text-foreground">not</span> collect or store your
            Steam password or Google password.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">How we use your information</h2>
          <p className="text-muted-foreground">We use your information to:</p>
          <ul className="list-disc space-y-2 pl-5 text-muted-foreground">
            <li>authenticate you and restrict access to approved friends;</li>
            <li>retrieve your Steam game library when requested by you;</li>
            <li>compare game libraries between friends;</li>
            <li>help organise co-op gaming sessions;</li>
            <li>maintain, debug, and secure the website.</li>
          </ul>
          <p className="text-muted-foreground">
            We do not sell your personal information or use it for advertising.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Steam data</h2>
          <p className="text-muted-foreground">
            Steam data is retrieved using the Steam Web API. We only retrieve Steam data for users
            who have chosen to connect or provide their Steam account information for use with this
            site.
          </p>
          <p className="text-muted-foreground">
            Steam data is provided &ldquo;as is&rdquo; by Steam/Valve. {appName} is not affiliated
            with, endorsed by, or sponsored by Valve Corporation or Steam.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Who can see your information</h2>
          <p className="text-muted-foreground">
            Information on the site is visible only to approved, signed-in members of our friend
            group. For example, other approved users may be able to see your Steam display name,
            shared games, and co-op session participation.
          </p>
          <p className="text-muted-foreground">
            We do not share your information with anyone outside the friend group except as needed
            to operate the site, such as through:
          </p>
          <ul className="list-disc space-y-2 pl-5 text-muted-foreground">
            <li>Auth0 and Google, for sign-in (Auth0 stores authentication data in the EU);</li>
            <li>Steam/Valve, for Steam Web API requests;</li>
            <li>our hosting/database provider, {hostingProvider}.</li>
          </ul>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Where your information is stored</h2>
          <p className="text-muted-foreground">
            The website and its data, including stored Steam data, are hosted in{" "}
            <span className="font-medium text-foreground">{storageCountry}</span>.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">How long we keep information</h2>
          <p className="text-muted-foreground">
            We keep your information while you are using the site or remain part of the allowlist.
            You can ask us to delete your account and associated data at any time.
          </p>
          <p className="text-muted-foreground">
            Server logs are kept for a limited period for security and debugging, usually no longer
            than {logRetentionDays} days unless needed to investigate a problem.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Deleting your data</h2>
          <p className="text-muted-foreground">
            You may request deletion of your data by contacting us at:
          </p>
          <p className="font-medium">{contactEmail}</p>
          <p className="text-muted-foreground">
            After deletion, your account, stored Steam data, and session information will be removed
            where reasonably possible. Some limited technical logs may remain temporarily until they
            expire.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Security</h2>
          <p className="text-muted-foreground">
            We take reasonable steps to protect the site and its data, including restricting access
            to approved Google accounts and keeping our Steam Web API key confidential.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Changes to this policy</h2>
          <p className="text-muted-foreground">
            We may update this Privacy Policy if the site changes. The latest version will be posted
            on this page.
          </p>
        </section>

        <section className="space-y-3">
          <h2 className="text-xl font-semibold tracking-tight">Contact</h2>
          <p className="text-muted-foreground">
            If you have questions or want your data removed, contact:
          </p>
          <p className="font-medium">
            {contactName}
            <br />
            {contactEmail}
          </p>
        </section>

        <div className="pt-4">
          <Link
            to="/"
            className="text-sm font-medium text-primary underline-offset-4 hover:underline"
          >
            Back to home
          </Link>
        </div>
      </div>
    </main>
  );
}
