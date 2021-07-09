<template>
  <div class="add-collection">
    <h1>Add Collection</h1>
    <form method="post">
      <input
        type="text"
        v-model="state.name"
        name="name"
        placeholder="Collection Name"
        required
      />
      <input type="submit" value="Add" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";

export default defineComponent({
  setup() {
    const state = reactive({
      name: "",
    });

    return {
      state,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
