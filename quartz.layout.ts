import { PageLayout, SharedLayout } from "./quartz/cfg"
import * as Component from "./quartz/components"

const isoDateRegex = /^\d{4}-\d{2}-\d{2}$/

const explorerSortFn = (
  a: { slugSegment: string; displayName: string; isFolder: boolean },
  b: { slugSegment: string; displayName: string; isFolder: boolean },
) => {
  // Pin "Software Civil Engineering" above "Daily D4 Digest"
  if (a.slugSegment === "software-civil-engineering") return -1
  if (b.slugSegment === "software-civil-engineering") return 1

  // Folders before files
  if (a.isFolder !== b.isFolder) return a.isFolder ? -1 : 1

  // Date-named files (YYYY-MM-DD) sort in reverse chronological order.
  // Match against slugSegment (the filename) rather than displayName (which is
  // the frontmatter title and may be prefixed, e.g. "Daily D4 Digest — 2026-05-15").
  if (isoDateRegex.test(a.slugSegment) && isoDateRegex.test(b.slugSegment)) {
    return b.slugSegment.localeCompare(a.slugSegment)
  }

  // Default: alphabetical
  return a.displayName.localeCompare(b.displayName, undefined, {
    numeric: true,
    sensitivity: "base",
  })
}

// components shared across all pages
export const sharedPageComponents: SharedLayout = {
  head: Component.Head(),
  header: [],
  afterBody: [Component.Lightbox()],
  footer: Component.Footer({
    links: {
      GitHub: "https://github.com/jackyzha0/quartz",
      "Discord Community": "https://discord.gg/cRFFHYye7t",
    },
  }),
}

// components for pages that display a single page (e.g. a single note)
export const defaultContentPageLayout: PageLayout = {
  beforeBody: [
    Component.ConditionalRender({
      component: Component.Breadcrumbs(),
      condition: (page) => page.fileData.slug !== "index",
    }),
    Component.ArticleTitle(),
    Component.ContentMeta(),
    Component.AuthorInfo({
      name: "Martin Rosén-Lidholm",
      portraitPath: "/chronograph/static/mrl-portrait.jpg",
      linkedinUrl: "https://www.linkedin.com/in/martin-rosen-lidholm/",
    }),
    Component.TagList(),
  ],
  left: [
    Component.DesktopOnly(Component.PageTitle()),
    Component.Flex({
      components: [
        {
          Component: Component.Search(),
          grow: true,
        },
        { Component: Component.Darkmode() },
        { Component: Component.ReaderMode() },
      ],
    }),
    Component.Explorer({ sortFn: explorerSortFn }),
    Component.DesktopOnly(Component.Graph()),
  ],
  right: [Component.DesktopOnly(Component.TableOfContents()), Component.Backlinks()],
}

// components for pages that display lists of pages  (e.g. tags or folders)
export const defaultListPageLayout: PageLayout = {
  beforeBody: [Component.Breadcrumbs(), Component.ArticleTitle(), Component.ContentMeta()],
  left: [
    Component.DesktopOnly(Component.PageTitle()),
    Component.Flex({
      components: [
        {
          Component: Component.Search(),
          grow: true,
        },
        { Component: Component.Darkmode() },
      ],
    }),
    Component.Explorer({ sortFn: explorerSortFn }),
  ],
  right: [],
}
