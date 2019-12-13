<template>
  <q-page class="q-px-md">
    <h3>{{$t('groceriesreferentialpage.title')}}</h3>
    <q-tabs
      v-model="activeTab"
      dense
      class="text-black"
      active-color="white"
      active-bg-color="primary"
      indicator-color="primary"
      align="justify"
      >
      <q-tab name="actions" label="Actions" icon="add_shopping_cart" />
      <q-tab name="items" label="Items" icon="shopping_cart"/>
    </q-tabs>

    <q-separator />

    <q-tab-panels v-model="activeTab" animated>
      <q-tab-panel name="actions">
        <div class="text-h6"><q-icon name="add_shopping_cart" />Actions</div>
        <q-list bordered separator>
          <q-item clickable v-ripple v-for="item in actions" :key="item.Id">
            <q-item-section avatar>
              <q-icon color="primary" :name="item.icon" />
            </q-item-section>
            <q-item-section>{{item.name}}</q-item-section>
            <q-item-section side>
              <q-item-label caption>aka: {{item.aliases}}</q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </q-tab-panel>

      <q-tab-panel name="items">
        <div class="text-h6"><q-icon name="shopping_cart" />Items</div>
        <q-list bordered separator>
          <q-item clickable v-ripple v-for="item in items" :key="item.Id">
            <q-item-section avatar>
              <q-icon color="primary" :name="item.icon" />
            </q-item-section>
            <q-item-section>{{item.name}}</q-item-section>
            <q-item-section side>
              <q-item-label caption>aka: {{item.aliases}}</q-item-label>
            </q-item-section>
          </q-item>
          </q-list>
      </q-tab-panel>
    </q-tab-panels>
  </q-page>
</template>

<script>
// import xss from 'xss'
// import { NotifySuccess, NotifyFailure } from 'data/notify'

import GroceryActionService from 'services/GroceryActionService'
import GroceryItemService from 'services/GroceryItemService'

export default {
  name: 'GroceriesReferentialPage',
  components: {
  },
  data: function () {
    return {
      activeTab: 'actions',
      actions: [],
      items: []
    }
  },
  methods: {
  },
  created: function () {
    const groceryActionService = new GroceryActionService()
    groceryActionService.doGet().then((response) => {
      this.actions = response
    }).catch((response) => {
      console.log('Error')
    })

    const groceryItemService = new GroceryItemService()
    groceryItemService.doGet().then((response) => {
      this.items = response
    }).catch((response) => {
      console.log('Error')
    })
  }
}
</script>
