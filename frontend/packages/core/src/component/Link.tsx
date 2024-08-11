import { FC } from 'react'
import { useTranslate } from '../hook'
import { NavLink } from 'react-router-dom'

export type LinkProps = {
  label: string | string[]
  to: string
}

export const Link: FC<LinkProps> = ({label, to}) => {
  const t = useTranslate()

  return (
    <NavLink to={to}>{t(...[label].flat())}</NavLink>
  )
}