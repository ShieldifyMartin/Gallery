<template>
  <div class="submit-photo">
    <h1>Submit photo</h1>
    <form method="post" @submit.prevent="handleSubmit">
      <transition name="fade">
        <img
          v-if="state.pictureBase64"
          :src="state.pictureBase64"
          class="uploaded-image"
          alt="upload"
        />
      </transition>
      <label for="file" class="upload-label">choose a picture</label>
      <input
        type="file"
        id="file"
        class="file"
        size="80"
        ref="picture"
        @change="uploadImage"
      />
      <input
        type="text"
        v-model="state.location"
        name="location"
        id="location"
        placeholder="Location"
      />
      <input
        type="text"
        v-model="state.description"
        name="description"
        placeholder="Description"
        required
      />
      <select v-model="state.categoryId" class="categories-dropdown">
        <option value="" selected>Select category</option>
        <option
          v-for="category in state.categories"
          :key="category.id"
          :value="category.id"
        >
          {{ category.title }}
        </option>
      </select>
      <input type="submit" value="Create" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import useAlert from "../../components/Alert/UseAlert";
import router from "../../router";
import store from "../../store";
import {
  postService,
  categoryService,
  fileService,
  signalRService,
} from "../../services/index";
import validate from "./validator";

export default defineComponent({
  setup() {
    const state = reactive({
      categories: [],
      picture: null,
      pictureBase64: null,
      location: "",
      description: "",
      categoryId: "",
    });

    watchEffect(async () => {
      const categories = await categoryService.get();
      state.categories = categories;
    });

    const handleSubmit = async () => {
      const { picture, location, description, categoryId } = state;
      const isCorrect = validate(state);

      if (!isCorrect) {
        return;
      }
      var responseObj = await postService.create(
        store.state.auth.state.token,
        picture,
        location,
        description,
        categoryId
      );

      if (responseObj.status == 401) {
        router.push("/login");
      } else if (responseObj.status >= 200 && responseObj.status < 300) {
        useAlert("Successful!", true);
        signalRService.returnPosts();
        router.push("/" + responseObj.postId);
      } else {
        useAlert("Something went wrong!", false);
      }
    };

    const uploadImage = (e) => {
      const file = fileService.handleFileUpload(e);
      if (file) {
        state.picture = file;
        var fr = new FileReader();
        fr.readAsDataURL(file);
        fr.onload = () => {
          state.pictureBase64 = fr.result;
        };
      }
    };

    return {
      state,
      handleSubmit,
      uploadImage,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
