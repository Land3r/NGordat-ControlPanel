<template>
  <div
    class="q-pa-md"
    style="max-width: 400px"
  >
    <q-form
      class="q-gutter-md"
    >
      <q-input
        v-model="form.name"
        label="Name"
        hint="Name of the item"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please type something']"
      />

      <q-input
        v-model="form.aliases"
        type="Aliases"
        label="Item's aliases"
        lazy-rules
      />
    </q-form>
  </div>
</template>

<script>
export default {
  name: 'GroceryItemForm',
  data: function () {
    return {
      form: {
        name: '',
        aliases: ''
      }
    }
  }
}
</script>
