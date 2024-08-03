import { say } from '@orbit/core'
import React, { useState } from 'react'

export const Example: React.FC = () => {
  const [showMessage, toggleShowMessage] = useState(false)
  return (
    <section>
      <h1>We're going to use common module</h1>
      <hr />
      <hr />
      {showMessage && say('Hi there!')}
    </section>
  )
}
