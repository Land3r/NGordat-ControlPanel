<template>
  <div class="testimonial-quote group on-right">
    <div class="quote-container">
      <blockquote>
        <p>
          <vue-typer
            :text="text"
            :repeat="repeat"
            :shuffle='true'
            :pre-type-delay='200'
            :type-delay='50'
            :pre-erase-delay='2000'
            :erase-delay='250'
            :erase-on-complete='false'
            initial-action='typing'
            erase-style='select-all'
            caret-animation='blink'>
          </vue-typer>
        </p>
      </blockquote>
      <!-- <cite><span>Kristi Bruno</span><br>
          Social Media Specialist<br>
          American College of Chest Physicians
      </cite> -->
    </div>
  </div>
</template>

<style>
.testimonial-quote {
  font-size: 16px;
}

.testimonial-quote blockquote {
  /* Negate theme styles */
  border: 0;
  margin: 0;
  padding: 0;

  background: none;
  color: gray;
  /* TODO: Trouver une belle font. */
  font-family: Georgia, serif;
  font-size: 1.5em;
  font-style: italic;
  line-height: 1.4 !important;
  margin: 0;
  position: relative;
  text-shadow: 0 1px white;
  z-index: 600;
}

.testimonial-quote blockquote * {
  box-sizing: border-box;
}

.testimonial-quote blockquote p {
  color: #75808a;
  line-height: 1.4 !important;
}

.testimonial-quote blockquote p:first-child:before {
  content: '\201C';
  color: #81bedb;
  font-size: 7.5em;
  font-weight: 700;
  opacity: .5;
  position: absolute;
  top: -.4em;
  left: -.2em;
  text-shadow: none;
  z-index: -300;
}

.testimonial-quote cite {
  color: gray;
  display: block;
  font-size: .8em;
}

.testimonial-quote cite span {
  color: #5e5e5e;
  font-size: 1em;
  font-style: normal;
  font-weight: 700;
  letter-spacing: 1px;
  text-transform: uppercase;
  text-shadow: 0 1px white;
}

.testimonial-quote {
  position: relative;
}

.testimonial-quote.right cite {
  text-align: right;
}
</style>

<script>
import { VueTyper } from 'vue-typer'

/**
 * TypedSpan component.
 * Allow to simulate typing of a text.
 */
export default {
  name: 'TypedSpan',
  props: {
    text: String,
    repeat: String
  },
  components: {
    'vue-typer': VueTyper
  }
}
</script>
