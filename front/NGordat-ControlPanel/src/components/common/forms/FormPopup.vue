<template>
  <q-dialog
    v-model="open"
    :persistent="persistent"
  >
    <q-card style="min-width: 350px">
      <q-form
        class="q-gutter-md"
        @submit="onSubmit"
        @reset="onReset"
      >
        <q-card-section>
          <div class="text-h6">
            {{ title }}
          </div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <slot />
        </q-card-section>

        <q-card-actions
          align="right"
          class="text-primary"
        >
          <div>
            <q-btn
              v-if="cancelbutton"
              v-close-popup
              flat
              label="Cancel"
              @click="$emit('cancel')"
            />
            <q-btn
              v-if="resetbutton"
              flat
              label="Reset"
              type="reset"
              @click="$emit('reset')"
            />
            <q-btn
              v-if="confirmbutton"
              v-close-popup
              label="Confirm"
              type="submit"
              color="primary"
              @click="$emit('confirm')"
            />
          </div>
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
export default {
  name: 'FormPopup',
  props: {
    title: {
      type: String,
      default: '',
      required: false
    },
    persistent: {
      type: Boolean,
      default: false,
      required: false
    },
    resetbutton: {
      type: Boolean,
      default: false,
      required: false
    },
    cancelbutton: {
      type: Boolean,
      default: false,
      required: false
    },
    confirmbutton: {
      type: Boolean,
      default: false,
      required: false
    }
  },
  data: function () {
    return {
      open: true,
      address: ''
    }
  },
  methods: {
    onSubmit: function () {
    },
    onReset: function () {
    }
  }
}
</script>
