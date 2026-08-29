import { QuartzComponent, QuartzComponentConstructor, QuartzComponentProps } from "./types"
import style from "./styles/authorInfo.scss"

interface Options {
  name: string
  portraitPath: string
}

export default ((opts: Options) => {
  const AuthorInfo: QuartzComponent = ({ fileData, displayClass }: QuartzComponentProps) => {
    if (fileData.slug === "index") return null
    return (
      <div class={`author-info ${displayClass ?? ""}`}>
        <img src={opts.portraitPath} alt={opts.name} class="author-portrait" />
        <div class="author-details">
          <span class="author-name">{opts.name}</span>
        </div>
      </div>
    )
  }

  AuthorInfo.css = style
  return AuthorInfo
}) satisfies QuartzComponentConstructor
